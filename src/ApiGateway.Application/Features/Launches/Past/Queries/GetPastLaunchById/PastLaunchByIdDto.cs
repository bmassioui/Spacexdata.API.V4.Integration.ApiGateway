namespace ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchById;

public sealed record PastLaunchByIdDto
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

public sealed record PastLaunchByIdMedias
{
    public string? SmallImage { get; set; }
    public string? LargeImage { get; set; }
    public string? Video { get; set; }
}