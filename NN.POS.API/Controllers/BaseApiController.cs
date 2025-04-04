using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NN.POS.API.Core.Utils;

namespace NN.POS.API.Controllers;

[Route("/api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize]
public class BaseApiController : ControllerBase
{
    private const string ResourceHeader = "X-Resource";
    private const string IdentityHeader = "X-Identity";


    protected int UserId {
        get
        {
            var id = User?.FindFirst("userId")?.Value;
            return id.ToInt();
        }
    }

    protected string UserRole
    {
        get
        {
            var pos = User?.FindFirst(ClaimTypes.Role)?.Value;
            return string.IsNullOrWhiteSpace(pos) ? "" : pos;
        }
    }

    protected ActionResult Accepted(string resource, string resourceId)
    {
        if (!string.IsNullOrWhiteSpace(resourceId))
        {
            Response.Headers.Append(ResourceHeader, $"{resource}/{resourceId}");
        }

        return base.Accepted();
    }

    protected ActionResult AcceptedWithResource(string resource, string identity)
    {
        if (string.IsNullOrWhiteSpace(resource))
        {
            return base.Accepted();
        }

        Response.Headers.Append(ResourceHeader, $"{resource}/{identity}");
        Response.Headers.Append(IdentityHeader, $"{identity}");

        return base.Accepted();
    }

    protected ActionResult OkWithResource(object obj, string resource, string identity)
    {
        if (string.IsNullOrWhiteSpace(resource))
        {
            return base.Ok(obj);
        }

        Response.Headers.Append(ResourceHeader, $"{resource}");
        Response.Headers.Append(IdentityHeader, $"{identity}");

        return base.Ok(obj);
    }
}