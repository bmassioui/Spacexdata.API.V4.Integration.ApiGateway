namespace ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchById;

public class PastLaunchByIdDto
{
    public int FlightNumber { get; set; }
    public string Name { get; set; } = null!;
    public string LaunchedAtUtc { get; set; } = null!;
    public string Rocket { get; set; } = null!;
    public bool? IsSuccess { get; set; }
    public string? Details { get; set; }
    public PastLaunchByIdMedias? Links { get; set; }
    public bool AutoUpdate { get; set; }
}

public class PastLaunchByIdMedias
{
    public string? SmallImage { get; set; }
    public string? LargeImage { get; set; }
    public string? Video { get; set; }
}