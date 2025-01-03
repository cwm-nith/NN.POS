﻿using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NN.POS.API.App.Commands.Users;
using NN.POS.API.App.Queries.Users;
using NN.POS.Common.Pagination;
using NN.POS.Model.Dtos.Users;

namespace NN.POS.API.Controllers.V1;

[ApiVersion("1")]
public class UserController(IMediator mediator) : BaseApiController
{

    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns></returns>

    [Authorize(Roles = "Admin,read-user")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<PagedResult<UserDto>>> Get([FromQuery] PagedQuery q)
    {
        var query = new GetUserQuery()
        {
            Page = q.Page,
            Results = q.Results,
        };
        var data = await mediator.Send(query);
        return Ok(data);
    }

    [Authorize(Roles = "Admin,read-user")]
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<UserDto>> GetUserById(int id)
    {
        var query = new GetUserByIdQuery(id);
        var data = await mediator.Send(query);
        return Ok(data);
    }

    [Authorize(Roles = "Admin,read-user")]
    [HttpGet("{username}/username")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<UserDto>> GetUserByUsername(string username)
    {
        var query = new GetUserByNameQuery(username);
        var data = await mediator.Send(query);
        return Ok(data);
    }

    [Authorize(Roles = "Admin,read-user")]
    [HttpGet("{email}/email")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
    {
        var query = new GetUserByEmailQuery(email);
        var data = await mediator.Send(query);
        return Ok(data);
    }

    [Authorize(Roles = "Admin,write-user")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto body)
    {
        var cmd = new CreateUserCommand(name: body.Name, username: body.Username, password: body.Password,
            email: body.Email, updatedAt: DateTime.UtcNow);
        var data = await mediator.Send(cmd);
        return Ok(data);
    }

    [Authorize(Roles = "Admin,write-user")]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<UserDto>> CreateUser(int id, [FromBody] UpdateUserDto body)
    {
        var cmd = new UpdateUserCommand(id, body.Name, body.Email);
        var data = await mediator.Send(cmd);
        return Ok(data);
    }

    [Authorize(Roles = "Admin,write-user")]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<bool>> DeleteUser(int id)
    {
        var cmd = new DeleteUserCommand(id);
        var data = await mediator.Send(cmd);
        return Ok(data);
    }
}