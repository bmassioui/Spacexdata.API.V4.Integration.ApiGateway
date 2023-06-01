namespace ApiGateway.Infrastructure.Models;

public sealed record GetPastLaunchesRequestModel
{
    public PastLaunchesRequestQueryOptions Query { get; set; } = null!;
    public PastLaunchesRequestOptions Options { get; set; } = null!;
}

public class PastLaunchesRequestQueryOptions
{
    public bool Upcoming { get; set; }
}

public record PastLaunchesRequestOptions
{
    public int Offset { get; set; }
    public int Limit { get; set; }
    public PastLaunchesRequestSortOptions? Sort { get; set; }
    public string[] Select { get; set; } = null!;
}

public record PastLaunchesRequestSortOptions
{
    public string Date_Utc { get; set; } = null!;
}
