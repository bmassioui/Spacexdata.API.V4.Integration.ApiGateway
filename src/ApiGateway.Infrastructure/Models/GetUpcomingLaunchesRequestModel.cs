namespace ApiGateway.Infrastructure.Models;

public sealed record GetUpcomingLaunchesRequestModel
{
    public UpcomingLaunchesRequestOptions Options { get; set; } = null!;
}

public record UpcomingLaunchesRequestOptions
{
    public int Offset { get; set; }
    public int Limit { get; set; }
    public UpcomingLaunchesRequestSortOptions? Sort { get; set; }
    public string[] Select { get; set; } = null!;
}

public record UpcomingLaunchesRequestSortOptions
{
    public string Date_Utc { get; set; } = null!;
}
