namespace ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchesWithPagination;

public sealed record PastLaunchesDto
{
    public PastLaunch[] PastLaunches { get; set; } = Array.Empty<PastLaunch>();
    public uint TotalPastLaunches { get; set; }
    public uint Offset { get; set; }
    public uint Limit { get; set; }
    public uint TotalPages { get; set; }
    public uint PagingCounter { get; set; }
    public bool HasPrevPage { get; set; }
    public bool HasNextPage { get; set; }
    public uint? PrevPage { get; set; }
    public uint? NextPage { get; set; }
}

public sealed record PastLaunch
{
    public string Id { get; set; } = null!;
    public PastLaunchMedias? Links { get; set; }
    public bool? IsSuccess { get; set; }
    public string? Details { get; set; }
    public int FlightNumber { get; set; }
    public string Name { get; set; } = null!;
    public DateTime? LaunchedAtUtc { get; set; }
}

public sealed record PastLaunchMedias
{
    public string? SmallImage { get; set; }
    public string? LargeImage { get; set; }
    public string? Video { get; set; }
}
