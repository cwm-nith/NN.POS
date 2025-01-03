﻿using NN.POS.API.App.Queries.BusinessPartners.CustomerGroups;
using NN.POS.API.Core.Entities.BusinessPartners.CustomerGroups;
using NN.POS.API.Infra.Tables.BusinessPartners;
using NN.POS.API.Infra.Tables;
using NN.POS.Common.Pagination;
using NN.POS.API.Core.IRepositories.BusinessPartners;
using NN.POS.API.Core.Exceptions.BusinessPartners.CustomerGroups;
using Microsoft.EntityFrameworkCore;

namespace NN.POS.API.Infra.Repositories.BusinessPartners;

public class CustomerGroupRepository(IReadDbRepository<CustomerGroupTable> readDbRepository,
    IWriteDbRepository<CustomerGroupTable> writeDbRepository) : ICustomerGroupRepository
{
    public async Task<PagedResult<CustomerGroupEntity>> GetAsync(GetCustomerGroupsQuery q,
        CancellationToken cancellationToken = default)
    {

        if (string.IsNullOrWhiteSpace(q.Search))
        {
            var data = await readDbRepository.BrowseDescAsync(i => true, i => i.CreatedAt, q, cancellationToken);
            return data.Map(i => i.ToEntity());
        }
        else
        {
            var data = await readDbRepository.BrowseDescAsync(i => EF.Functions.Like(i.Name, $"%{q.Search}%"), i => i.CreatedAt, q, cancellationToken);
            return data.Map(i => i.ToEntity());
        }
    }

    public async Task<CustomerGroupEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var data = await readDbRepository.FirstOrDefaultAsync(i => i.Id == id, cancellationToken) ??
                   throw new CustomerGroupNotFoundException();
        return data.ToEntity();
    }

    public async Task UpdateAsync(CustomerGroupEntity entity, CancellationToken cancellationToken = default)
    {
        var isExisted = await readDbRepository.ExistsAsync(i => i.Id == entity.Id, cancellationToken);
        if (!isExisted) throw new CustomerGroupNotFoundException();

        await writeDbRepository.UpdateAsync(entity.ToTable(), cancellationToken);
    }

    public async Task AddAsync(CustomerGroupEntity entity, CancellationToken cancellationToken = default)
    {
        await writeDbRepository.AddAsync(entity.ToTable(), cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var isExisted = await readDbRepository.ExistsAsync(i => i.Id == id, cancellationToken);
        if (!isExisted) throw new CustomerGroupNotFoundException();

        await writeDbRepository.DeleteAsync(id, cancellationToken);
    }
}