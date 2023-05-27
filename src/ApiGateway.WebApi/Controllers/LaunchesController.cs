using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace ApiGateway.WebApi.Controllers;

public class LaunchesController : ApiControllerBase
{
    /// <summary>
    /// Past launches
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name = "/", Order = 0)]
    [OutputCache]
    public IActionResult GetPastLaunches()
    {
        return Ok();
    }

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
