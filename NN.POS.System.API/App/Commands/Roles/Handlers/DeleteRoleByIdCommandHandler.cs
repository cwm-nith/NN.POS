﻿using MediatR;
using NN.POS.System.API.Core.IRepositories.Roles;

namespace NN.POS.System.API.App.Commands.Roles.Handlers;

public class DeleteRoleByIdCommandHandler(IRoleRepository roleRepository) : IRequestHandler<DeleteRoleByIdCommand, bool>
{
    public async Task<bool> Handle(DeleteRoleByIdCommand request, CancellationToken cancellationToken)
    {
        return await roleRepository.DeleteRoleAsync(request.Id, cancellationToken);
    }
}