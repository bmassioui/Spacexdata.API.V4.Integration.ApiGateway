using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.WebApi.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[ApiVersion("4.0")]
public abstract class ApiControllerBase : ControllerBase
{ }
