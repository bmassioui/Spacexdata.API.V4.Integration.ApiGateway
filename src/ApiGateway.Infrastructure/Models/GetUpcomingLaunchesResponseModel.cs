namespace ApiGateway.Infrastructure.Models;

public sealed class GetUpcomingLaunchesResponseModel
{
    public UpcomingLaunchesResponse[] Docs { get; set; } = null!;
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

public sealed record UpcomingLaunchesResponse
{
    public int Flight_number { get; set; }
    public string Name { get; set; } = null!;
    public DateTime? Date_utc { get; set; }
    public string Id { get; set; } = null!;
}
