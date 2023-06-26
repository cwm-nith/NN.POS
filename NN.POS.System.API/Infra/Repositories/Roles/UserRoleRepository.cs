﻿using Microsoft.EntityFrameworkCore;
using NN.POS.System.API.Core.Exceptions.Roles;
using NN.POS.System.API.Core.Exceptions.UserRoles;
using NN.POS.System.API.Core.Exceptions.Users;
using NN.POS.System.API.Core.IRepositories.Roles;
using NN.POS.System.API.Infra.Tables;
using NN.POS.System.API.Infra.Tables.Roles;
using NN.POS.System.Model.Dtos.Roles;

namespace NN.POS.System.API.Infra.Repositories.Roles;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly IWriteDbRepository<UserRoleTable> _writeDbRepository;
    private readonly IReadDbRepository<UserRoleTable> _readDbRepository;
    private readonly IRoleRepository _roleRepository;

    public UserRoleRepository(IWriteDbRepository<UserRoleTable> writeDbRepository, IRoleRepository roleRepository,
        IReadDbRepository<UserRoleTable> readDbRepository)
    {
        _writeDbRepository = writeDbRepository;
        _roleRepository = roleRepository;
        _readDbRepository = readDbRepository;
    }

    public async Task<bool> AddRoleToUserAsync(int userId, int roleId, CancellationToken cancellation = default)
    {
        var isRoleExisted = await _roleRepository.IsRoleExistedAsync(roleId, cancellation);
        if (!isRoleExisted) throw new RoleNotFoundException(roleId);

        var isUserExisted = _readDbRepository.Context.Users != null && await _readDbRepository.Context.Users.AnyAsync(i => i.Id == userId, cancellation);
        if (!isUserExisted) throw new UserNotFoundException(userId);

        if (await UserRoleExistedAsync(userId, roleId, cancellation)) throw new UserRoleAlreadyExistedException(userId, roleId);

        var role = new UserRoleTable(0, userId, roleId);

        await _writeDbRepository.AddAsync(role, cancellation);
        return true;
    }

    public Task<bool> UserRoleExistedAsync(int userId, int roleId, CancellationToken cancellation = default)
    {
        return _readDbRepository.ExistsAsync(i => i.RoleId == roleId && i.UserId == userId, cancellation);
    }

    public async Task<bool> RemoveUserRoleAsync(int userId, int roleId, CancellationToken cancellation = default)
    {
        var isRoleExisted = await _roleRepository.IsRoleExistedAsync(roleId, cancellation);
        if (!isRoleExisted) throw new RoleNotFoundException(roleId);

        var isUserExisted = _readDbRepository.Context.Users != null && await _readDbRepository.Context.Users.AnyAsync(i => i.Id == userId, cancellation);
        if (!isUserExisted) throw new UserNotFoundException(userId);

        if (!(await UserRoleExistedAsync(userId, roleId, cancellation))) throw new UserRoleNotExistedException(userId, roleId);
        var role = await _readDbRepository.FirstOrDefaultAsync(i => i.UserId == userId && i.RoleId == roleId, cancellation) ?? throw new UserRoleNotFoundException();
        var num = await _writeDbRepository.DeleteAsync(role.Id, cancellation);
        return num > 0;
    }

    public async Task<List<UserRoleDto>> GetUserRolesAsync(int userId, CancellationToken cancellation = default)
    {
        var context = _readDbRepository.Context;
        var data = await (from userRole in context.UserRoles!
                          join role in context.Roles! on userRole.RoleId equals role.Id
                          where userRole.UserId == userId
                          select new UserRoleDto(
                                  userRole.Id,
                                  userId,
                                  role.Id,
                                  role.Name,
                                  role.CreatedAt,
                                  role.UpdatedAt
                                  )
                          {
                              DisplayName = role.DisplayName,
                              Description = role.Description,
                          })
            .ToListAsync(cancellation);

        return data;
    }

    public async Task<List<UserRoleDto>> GetAllUserRolesAsync(int userId, CancellationToken cancellation = default)
    {
        var context = _readDbRepository.Context;
        var data = await (from role in context.Roles!
                          join userRole in context.UserRoles!.Where(i => i.UserId == userId) on role.Id equals userRole.RoleId into g
                          from ur in g.DefaultIfEmpty()
                          select new UserRoleDto(
                              ur != null ? ur.Id : 0,
                              ur != null ? ur.UserId : userId,
                              role.Id,
                              role.Name,
                              role.CreatedAt,
                              role.UpdatedAt
                          )
                          {
                              IsInRole = ur != null,
                              DisplayName = role.DisplayName,
                              Description = role.Description,
                          })
            .ToListAsync(cancellation);

        return data;
    }
}