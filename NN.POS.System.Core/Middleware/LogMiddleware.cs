﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NN.POS.System.Core.Exceptions.Middleware;

namespace NN.POS.System.Core.Middleware;

public class LogMiddleware
{
    private readonly ILogger<ErrorHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public LogMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var clientId = context.Request.Headers["X-ClientId"].ToString();
        var clientIp = context.Request.Headers["X-Real-IP"].ToString();
        var userId = context.User.Identity?.Name;


        _logger.LogInformation("Requested from ip: {@IP}, client id: {@ClientId}, user id: {@UserId}", clientIp, clientId,
            userId ?? string.Empty);
        await _next(context);
    }
}