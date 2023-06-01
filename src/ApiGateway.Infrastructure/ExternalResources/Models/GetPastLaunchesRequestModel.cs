namespace ApiGateway.Infrastructure.ExternalResources.Models;

public sealed record GetPastLaunchesRequestModel
{
    public Options Options { get; set; } = null!;
}

public record Options
{
    public int Offset { get; set; }
    public int Limit { get; set; }
    public Sort? Sort { get; set; }
    public string[] Select { get; set; } = null!;
}

public record Sort
{
    public string Date_Utc { get; set; } = null!;
}
