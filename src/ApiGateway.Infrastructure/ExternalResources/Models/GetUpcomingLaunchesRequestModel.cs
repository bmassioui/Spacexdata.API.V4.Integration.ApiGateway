namespace ApiGateway.Infrastructure.ExternalResources.Models;

public sealed record GetUpcomingLaunchesRequestModel
{
    public UpcomingLaunchesRequestOptionsWrapper Options { get; set; } = null!;
}

public record UpcomingLaunchesRequestOptionsWrapper
{
    public int Offset { get; set; }
    public int Limit { get; set; }
    public UpcomingLaunchesRequestOptionsSortWrapper? Sort { get; set; }
    public string[] Select { get; set; } = null!;
}

public record UpcomingLaunchesRequestOptionsSortWrapper
{
    public string Date_Utc { get; set; } = null!;
}
