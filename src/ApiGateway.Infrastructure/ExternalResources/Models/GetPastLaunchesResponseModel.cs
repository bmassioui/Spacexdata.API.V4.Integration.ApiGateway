namespace ApiGateway.Infrastructure.ExternalResources.Models;

public sealed class GetPastLaunchesResponseModel
{
    public PastLaunchesResponseWrapper[] Docs { get; set; } = null!;
}

public class PastLaunchesResponseWrapper
{
    public PastLaunchesResponseLinksWrapper? Links { get; set; }
    public bool? Success { get; set; }
    public string? Details { get; set; }
    public int Flight_number { get; set; }
    public string Name { get; set; } = null!;
    public DateTime? Date_utc { get; set; }
    public string Id { get; set; } = null!;
}

public class PastLaunchesResponseLinksWrapper
{
    public PastLaunchesResponseLinksPatchWrapper? Patch { get; set; }
    public string? Webcast { get; set; }
}

public class PastLaunchesResponseLinksPatchWrapper
{
    public string? Small { get; set; }
    public string? Large { get; set; }
}
