namespace ApiGateway.Infrastructure.ExternalResources.Models;

public sealed class GetUpcomingLaunchesResponseModel
{
    public UpcomingLaunchesResponseWrapper[] Docs { get; set; } = null!;
}

public class UpcomingLaunchesResponseWrapper
{
    public int Flight_number { get; set; }
    public string Name { get; set; } = null!;
    public DateTime? Date_utc { get; set; }
    public string Id { get; set; } = null!;
}
