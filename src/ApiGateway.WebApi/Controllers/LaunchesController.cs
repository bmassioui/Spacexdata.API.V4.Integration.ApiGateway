﻿using ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchesWithPagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace ApiGateway.WebApi.Controllers;

public class LaunchesController : ApiControllerBase
{
    /// <summary>
    /// Past launches sorted and paginated
    /// </summary>
    /// <returns>Past launches sorted and paginated</returns>
    /// <response code="200">Query has been performed successfully</response>
    /// <response code="400">Invalid request parameters</response>
    [HttpGet(Name = "/", Order = 0)]
    [OutputCache]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IReadOnlyList<PastLaunchesDto>>> GetPastLaunches() =>
        Ok(await Mediator.Send(new GetPastLaunchesWithPaginationQuery()));

    /// <summary>
    /// Past launch by id
    /// </summary>
    /// <returns></returns>
    //[HttpGet(Name = "/{id:string}", Order = 1)]
    //[OutputCache]
    //public IActionResult GetPastLaunchById()
    //{
    //    return Ok();
    //}

    /// <summary>
    /// Upcoming launches
    /// </summary>
    /// <returns></returns>
    //[HttpGet(Name = "/Upcoming", Order = 2)]
    //[OutputCache]
    //public IActionResult GetUpcomingLaunches()
    //{
    //    return Ok();
    //}

    /// <summary>
    /// Upcoming launch by id
    /// </summary>
    /// <returns></returns>
    //[HttpGet(Name = "/Upcoming/{id:string}", Order = 3)]
    //[OutputCache]
    //public IActionResult GetUpcomingLaunchesById()
    //{
    //    return Ok();
    //}
}