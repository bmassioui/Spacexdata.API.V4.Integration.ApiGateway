namespace ApiGateway.Infrastructure.Models;

public sealed record GetPastLaunchesResponseModel
{
    public PastLaunchesResponse[] Docs { get; set; } = null!;
    public uint TotalDocs { get; set; }
    public uint Offset { get; set; }
    public uint Limit { get; set; }
    public uint TotalPages { get; set; }
    public uint PagingCounter { get; set; }
    public bool HasPrevPage { get; set; }
    public bool HasNextPage { get; set; }
    public uint? PrevPage { get; set; }
    public uint? NextPage { get; set; }
}

public sealed record PastLaunchesResponse
{
    public PastLaunchesResponseLinks? Links { get; set; }
    public bool? Success { get; set; }
    public string? Details { get; set; }
    public int Flight_number { get; set; }
    public string Name { get; set; } = null!;
    public DateTime? Date_utc { get; set; }
    public string Id { get; set; } = null!;
}

public sealed record PastLaunchesResponseLinks
{
    public PastLaunchesResponseLinksPatch? Patch { get; set; }
    public string? Webcast { get; set; }
}

public sealed record PastLaunchesResponseLinksPatch
{
    public string? Small { get; set; }
    public string? Large { get; set; }
}
