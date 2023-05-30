namespace ApiGateway.Application.Options;
public sealed class SpaceXWebApiOptions
{
    public string BaseUrl { get; set; } = null!;
    public LaunchesOptions Launches { get; set; } = null!;
}

public sealed class LaunchesOptions
{
    public string PostQueryEndPointUri { get; set; } = null!;
    public string GetByIdEndPointUri { get; set; } = null!;
}
