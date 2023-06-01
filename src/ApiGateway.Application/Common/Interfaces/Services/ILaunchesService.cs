using ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchesWithPagination;
using ApiGateway.Application.Features.Launches.Upcoming.Queries.GetUpcomingLaunchesWithPagination;

namespace ApiGateway.Application.Common.Interfaces.Services;

public interface ILaunchesService
{
    /// <summary>
    /// Past launches sorted and paginated - Async
    /// </summary>
    /// <param name="offset">Skip position</param>
    /// <param name="limit">Number of items</param>
    /// <returns>Past launches sorted and paginated</returns
    Task<PastLaunchesDto?> GetPastLaunchchesAsync(ushort offset, ushort limit, CancellationToken cancellationToken = default);

    /// <summary>
    /// Upcoming launches sorted and paginated - Async
    /// </summary>
    /// <param name="offset">Skip position</param>
    /// <param name="limit">Number of items</param>
    /// <returns>Upcoming launches sorted and paginated</returns
    Task<UpcomingLaunchesDto?> GetUpcomingLaunchchesAsync(ushort offset, ushort limit, CancellationToken cancellationToken = default);
}