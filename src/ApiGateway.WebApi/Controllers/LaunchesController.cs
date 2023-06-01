using ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchesWithPagination;
using ApiGateway.Application.Features.Launches.Upcoming.Queries.GetUpcomingLaunchesWithPagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ApiGateway.WebApi.Controllers;

public class LaunchesController : ApiControllerBase
{
    /// <summary>
    /// Past launches sorted and paginated
    /// </summary>
    /// <param name="offset">Skip position</param>
    /// <param name="limit">Number of items</param>
    /// <returns>Past launches sorted and paginated</returns>
    /// <response code="200">Query has been performed successfully</response>
    /// <response code="400">Invalid request parameters</response>
    [HttpGet(template: "/past", Name = "GetPastLaunches", Order = 0)]
    [OutputCache]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UpcomingLaunchesDto>> GetPastLaunches(ushort? offset, ushort? limit) =>
        Ok(await Mediator.Send(new GetPastLaunchesWithPaginationQuery(offset, limit)));

    /// <summary>
    /// Past launch by id
    /// </summary>
    /// <returns></returns>
    [HttpGet(template: "/past/{id}", Name = "GetPastLaunchById", Order = 1)]
    [OutputCache]
    public IActionResult GetPastLaunchById([Required] string id)
    {
        return Ok();
    }

    /// <summary>
    /// Upcoming launches sorted and paginated
    /// </summary>
    /// <param name="offset">Skip position</param>
    /// <param name="limit">Number of items</param>
    /// <returns>Upcoming launches sorted and paginated</returns>
    /// <response code="200">Query has been performed successfully</response>
    /// <response code="400">Invalid request parameters</response>
    [HttpGet(template: "/upcoming", Name = "GetUpcomingLaunches", Order = 2)]
    [OutputCache]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UpcomingLaunchesDto>> GetUpcomingLaunches(ushort? offset, ushort? limit) =>
        Ok(await Mediator.Send(new GetUpcomingLaunchesWithPaginationQuery(offset, limit)));

    /// <summary>
    /// Upcoming launch by id
    /// </summary>
    /// <returns></returns>
    [HttpGet(template: "/upcoming/{id}", Name = "GetUpcomingLaunchesById", Order = 3)]
    [OutputCache]
    public IActionResult GetUpcomingLaunchesById([Required] string id)
    {
        return Ok();
    }
}