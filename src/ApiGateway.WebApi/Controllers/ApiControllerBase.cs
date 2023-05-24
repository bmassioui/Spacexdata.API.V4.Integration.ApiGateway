using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public abstract class ApiControllerBase : ControllerBase
{
}
