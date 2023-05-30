using ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchesWithPagination;

namespace ApiGateway.Application.Common.Interfaces.Services;

public interface ILaunchesService
{
    /// <summary>
    /// Get past launches - Async
    /// </summary>
    /// <returns>Readonly list of past launches</returns>
    Task<PastLaunchesDto?> GetPastLaunchchesAsync(CancellationToken cancellationToken = default);
}