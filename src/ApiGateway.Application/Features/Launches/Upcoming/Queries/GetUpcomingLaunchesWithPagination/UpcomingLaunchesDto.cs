namespace ApiGateway.Application.Features.Launches.Upcoming.Queries.GetUpcomingLaunchesWithPagination;

public class UpcomingLaunchesDto
{
    public UpcomingLaunch[] UpcomingLaunches { get; set; } = Array.Empty<UpcomingLaunch>();
}

public class UpcomingLaunch
{
    public string Id { get; set; } = null!;
    public int FlightNumber { get; set; }
    public string Name { get; set; } = null!;
    public DateTime? LaunchedAtUtc { get; set; }
}
