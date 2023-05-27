using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace ApiGateway.WebApi.Controllers;

public class LaunchesController : ApiControllerBase
{
    private readonly ILogger<LaunchesController> _logger;

    public LaunchesController(ILogger<LaunchesController> logger) => _logger = logger;

    /// <summary>
    /// Past launches
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name = "/", Order = 0)]
    [OutputCache]
    public IActionResult GetPastLaunches()
    {
        _logger.LogInformation("Starting request:{@EndPointName}", nameof(GetPastLaunches));

        var employees = new List<Employee>
        {
            new Employee { Name = "John Doe", Age = 30, Department = "Engineering" },
            new Employee { Name = "Jane Smith", Age = 35, Department = "Marketing" },
            new Employee { Name = "Mike Johnson", Age = 28, Department = "Sales" },
            new Employee { Name = "Emily Brown", Age = 32, Department = "HR" },
            new Employee { Name = "David Wilson", Age = 40, Department = "Finance" }
        };

        _logger.LogInformation("Completed request:{@EndPointName}", nameof(GetPastLaunches));

        return Ok(employees);
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

/// <summary>
/// TOD: to remove later, just for test purpose
/// </summary>
public class Employee
{
    public string? Name { get; set; }
    public int Age { get; set; }
    public string? Department { get; set; }
}