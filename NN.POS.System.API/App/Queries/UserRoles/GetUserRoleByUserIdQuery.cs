﻿using MediatR;
using NN.POS.System.Model.Dtos.Roles;

namespace NN.POS.System.API.App.Queries.UserRoles;

public class GetUserRoleByUserIdQuery : IRequest<List<UserRoleDto>>
{
    public int UserId { get; set; }

    public GetUserRoleByUserIdQuery(int userId)
    {
        UserId = userId;
    }
}