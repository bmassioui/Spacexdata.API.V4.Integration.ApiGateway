﻿namespace ApiGateway.Application.Features.Launches.Upcoming.Queries.GetUpcomingLaunchesWithPagination;

public sealed record UpcomingLaunchesDto
{
    public UpcomingLaunch[] UpcomingLaunches { get; set; } = Array.Empty<UpcomingLaunch>();
    public uint TotalUpcomingLaunches { get; set; }
    public uint Offset { get; set; }
    public uint Limit { get; set; }
    public uint TotalPages { get; set; }
    public uint PagingCounter { get; set; }
    public bool HasPrevPage { get; set; }
    public bool HasNextPage { get; set; }
    public uint? PrevPage { get; set; }
    public uint? NextPage { get; set; }
}

public sealed record UpcomingLaunch
{
    public string Id { get; set; } = null!;
    public int FlightNumber { get; set; }
    public string Name { get; set; } = null!;
    public DateTime? LaunchedAtUtc { get; set; }
}
