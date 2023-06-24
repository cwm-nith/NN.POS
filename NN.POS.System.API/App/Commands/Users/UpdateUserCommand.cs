﻿using MediatR;
using NN.POS.System.API.Core.Dtos.Users;

namespace NN.POS.System.API.App.Commands.Users;

public class UpdateUserCommand : IRequest<UserDto>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }

    public UpdateUserCommand(int id, string? name, string? email)
    {
        Id = id;
        Name = name;
        Email = email;
    }
}