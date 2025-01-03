﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace NN.POS.API.Infra.Tables;

public class WriteDbRepository<TTable>(DataDbContext context, ILogger? logger) : IWriteDbRepository<TTable> where TTable : BaseTable
{
    public DataDbContext Context => context;
    public async Task<TTable> AddAsync(TTable entity, CancellationToken cancellation = default)
    {
        Context.Set<TTable>().Add(entity);
        await Context.SaveChangesAsync(cancellation);

        return entity;
    }

    public async Task<bool> AddManyAsync(List<TTable> entities, CancellationToken cancellation = default)
    {
        await Context.Set<TTable>().AddRangeAsync(entities, cancellation);
        await Context.SaveChangesAsync(cancellation);
        return true;
    }

    public Task UpdateAsync(TTable entity, CancellationToken cancellation = default)
    {
        Context.Entry(entity).State = EntityState.Modified;
        Context.Set<TTable>().Update(entity);
        return Context.SaveChangesAsync(cancellation);
    }

    public async Task<bool> UpdateManyAsync(List<TTable> entities, CancellationToken cancellation = default)
    {
        try
        {
            var strategy = Context.Database.CreateExecutionStrategy();
            await strategy.Execute(async () =>
            {
                await using var t = await Context.Database.BeginTransactionAsync(cancellation);
                foreach (var entity in entities)
                {
                    if (entity.Id > 0)
                    {
                        Context.Entry(entity).State = EntityState.Modified;
                        Context.Set<TTable>().Update(entity);
                    }
                    else
                    {
                        Context.Entry(entity).State = EntityState.Added;
                        await Context.Set<TTable>().AddAsync(entity, cancellation);
                    }
                    
                    await Context.SaveChangesAsync(cancellation);
                }

                await t.CommitAsync(cancellation);
            });
            return true;
        }
        catch (Exception ex)
        {
            logger?.LogDebug("{Message}", ex.Message);
        }
        return false;
    }

    public Task<int> DeleteAsync(int id, CancellationToken cancellation = default)
    {
        return DeleteAsync(x => x.Id == id, cancellation);
    }

    public Task<int> DeleteAsync(TTable entity, CancellationToken cancellation = default)
    {
        Context.Set<TTable>().Remove(entity);
        return Context.SaveChangesAsync(cancellation);
    }

    public Task<int> DeleteAsync(Expression<Func<TTable, bool>> predicate, CancellationToken cancellation = default)
    {
        var collection = Context.Set<TTable>().Where(predicate);
        Context.Set<TTable>().RemoveRange(collection);

        return Context.SaveChangesAsync(cancellation);
    }
}