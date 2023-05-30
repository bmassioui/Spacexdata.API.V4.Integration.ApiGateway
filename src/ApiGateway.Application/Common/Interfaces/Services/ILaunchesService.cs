using ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchesWithPagination;

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
}