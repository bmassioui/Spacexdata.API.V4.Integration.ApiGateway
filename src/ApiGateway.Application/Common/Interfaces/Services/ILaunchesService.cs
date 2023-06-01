using ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchById;
using ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchesWithPagination;
using ApiGateway.Application.Features.Launches.Upcoming.Queries.GetUpcomingLaunchById;
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
    Task<PastLaunchesDto?> GetPastLaunchesAsync(ushort offset, ushort limit, CancellationToken cancellationToken = default);

    /// <summary>
    /// Upcoming launches sorted and paginated - Async
    /// </summary>
    /// <param name="offset">Skip position</param>
    /// <param name="limit">Number of items</param>
    /// <returns>Upcoming launches sorted and paginated</returns
    Task<UpcomingLaunchesDto?> GetUpcomingLaunchesAsync(ushort offset, ushort limit, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get past launch by Id - Async
    /// </summary>
    /// <param name="id">Past launch Id</param>
    /// <returns>Past launch if exists else null</returns>
    Task<PastLaunchByIdDto?> GetPastLaunchByIdAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get upcoming launch by Id - Async
    /// </summary>
    /// <param name="id">Upcoming launch Id</param>
    /// <returns>Upcoming launch if exists else null</returns>
    Task<UpcomingLaunchByIdDto?> GetUpcomingLaunchByIdAsync(string id, CancellationToken cancellationToken = default);
}