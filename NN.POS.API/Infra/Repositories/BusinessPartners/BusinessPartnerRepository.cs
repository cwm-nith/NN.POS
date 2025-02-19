﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NN.POS.API.Core.Entities.BusinessPartners;
using NN.POS.API.Core.Exceptions.BusinessPartners;
using NN.POS.API.Core.IRepositories.BusinessPartners;
using NN.POS.API.Infra.Tables;
using NN.POS.API.Infra.Tables.BusinessPartners;
using NN.POS.Common.Pagination;

namespace NN.POS.API.Infra.Repositories.BusinessPartners;

public class BusinessPartnerRepository(IReadDbRepository<BusinessPartnerTable> readDbRepository,
    IWriteDbRepository<BusinessPartnerTable> writeDbRepository) : IBusinessPartnerRepository
{
    public async Task<BusinessPartnerEntity> CreateAsync(BusinessPartnerEntity entity, CancellationToken cancellation = default)
    {
        var bus = await writeDbRepository.AddAsync(entity.ToTable(), cancellation);
        return bus.ToEntity();
    }

    public async Task UpdateAsync(BusinessPartnerEntity entity, CancellationToken cancellation = default)
    {
        await writeDbRepository.UpdateAsync(entity.ToTable(), cancellation);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellation = default)
    {
        var num = await writeDbRepository.DeleteAsync(id, cancellation);
        return num > 0;
    }

    public async Task<BusinessPartnerEntity> GetByIdAsync(int id, CancellationToken cancellation = default)
    {
        var data = await readDbRepository.FirstOrDefaultAsync(i => i.Id == id, cancellation) ?? throw new BusinessPartnerNotFoundException(id);
        return data.ToEntity();
    }

    public async Task<int> GetCountAsync(CancellationToken cancellation = default)
    {
        return await readDbRepository.Context.BusinessPartners!.CountAsync(cancellation);
    }

    public async Task<PagedResult<BusinessPartnerEntity>> GetAllAsync(Expression<Func<BusinessPartnerTable, bool>> predicate, PagedQuery q, CancellationToken cancellation = default)
    {
        var data = await readDbRepository.BrowseAsync(predicate, o => o.CreatedAt, q, cancellation);
        return data.Map(i => i.ToEntity());
    }
}