namespace ApiGateway.Infrastructure.Models;

public sealed record GetUpcomingLaunchResponseModel
{
    public int FlightNumber { get; set; }
    public string Name { get; set; } = null!;
    public string Date_utc { get; set; } = null!;
    public string Rocket { get; set; } = null!;
    public bool? Success { get; set; }
    public string? Details { get; set; }
    public UpcomingLaunchResponseLinks? Links { get; set; }
    public bool AutoUpdate { get; set; }
}

public sealed record UpcomingLaunchResponseLinks
{
    public UpcomingLaunchResponsePatch? Patch { get; set; }
    public string? Webcast { get; set; }
}

public sealed record UpcomingLaunchResponsePatch
{
    public string? Small { get; set; }
    public string? Large { get; set; }
}
