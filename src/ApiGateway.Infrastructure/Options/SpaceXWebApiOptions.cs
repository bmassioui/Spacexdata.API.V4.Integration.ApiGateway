namespace ApiGateway.Infrastructure.Options;
public sealed record SpaceXWebApiOptions
{
    public string BaseUrl { get; set; } = null!;
    public LaunchesOptions Launches { get; set; } = null!;
}

public sealed record LaunchesOptions
{
    public string PostQueryEndPointUri { get; set; } = null!;
    public string GetByIdEndPointUri { get; set; } = null!;
}
