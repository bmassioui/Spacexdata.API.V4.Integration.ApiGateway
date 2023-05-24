using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.WebApi.Controllers;

public class LaunchesController : ApiControllerBase
{
    /// <summary>
    /// Past launches
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name = "/", Order = 0)]
    public IActionResult GetPastLaunches()
    {
        return Ok();
    }

    /// <summary>
    /// Past launch by id
    /// </summary>
    /// <returns></returns>
    //[HttpGet(Name = "/{id:string}", Order = 1)]
    //public IActionResult GetPastLaunchById()
    //{
    //    return Ok();
    //}

    /// <summary>
    /// Upcoming launches
    /// </summary>
    /// <returns></returns>
    //[HttpGet(Name = "/Upcoming", Order = 2)]
    //public IActionResult GetUpcomingLaunches()
    //{
    //    return Ok();
    //}

    /// <summary>
    /// Upcoming launch by id
    /// </summary>
    /// <returns></returns>
    //[HttpGet(Name = "/Upcoming/{id:string}", Order = 3)]
    //public IActionResult GetUpcomingLaunchesById()
    //{
    //    return Ok();
    //}
}
