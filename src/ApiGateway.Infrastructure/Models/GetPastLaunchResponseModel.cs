namespace ApiGateway.Infrastructure.Models;

public class GetPastLaunchResponseModel
{
    public int FlightNumber { get; set; }
    public string Name { get; set; } = null!;
    public string Date_utc { get; set; } = null!;
    public string Rocket { get; set; } = null!;
    public bool? Success { get; set; }
    public string? Details { get; set; }
    public PastLaunchResponseLinks? Links { get; set; }
    public bool AutoUpdate { get; set; }
}


public class PastLaunchResponseLinks
{
    public PastLaunchResponsePatch? Patch { get; set; }
    public string? Webcast { get; set; }
}

public class PastLaunchResponsePatch
{
    public string? Small { get; set; }
    public string? Large { get; set; }
}
