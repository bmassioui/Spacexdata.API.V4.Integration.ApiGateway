namespace ApiGateway.Infrastructure.ExternalResources.Models;

public sealed record GetPastLaunchesRequestModel
{
    public PastLaunchesRequestOptionsWrapper Options { get; set; } = null!;
}

public record PastLaunchesRequestOptionsWrapper
{
    public int Offset { get; set; }
    public int Limit { get; set; }
    public PastLaunchesRequestOptionsSortWrapper? Sort { get; set; }
    public string[] Select { get; set; } = null!;
}

public record PastLaunchesRequestOptionsSortWrapper
{
    public string Date_Utc { get; set; } = null!;
}
