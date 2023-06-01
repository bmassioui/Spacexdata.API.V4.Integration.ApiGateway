namespace ApiGateway.Infrastructure.Models;

public sealed record GetUpcomingLaunchesRequestModel
{
    public UpcomingLaunchesRequestOptions Options { get; set; } = null!;
}

public sealed record UpcomingLaunchesRequestOptions
{
    public int Offset { get; set; }
    public int Limit { get; set; }
    public UpcomingLaunchesRequestSort? Sort { get; set; }
    public string[] Select { get; set; } = null!;
}

public sealed record UpcomingLaunchesRequestSort
{
    public string Date_Utc { get; set; } = null!;
}
