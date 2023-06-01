using ApiGateway.WebApi.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ApiGateway.WebApi.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
[ApiVersion(Constants.ApiVersion)]
[EnableRateLimiting(Constants.FixedWindowLimiterPolicyName)]
[ApiExceptionFilter]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
