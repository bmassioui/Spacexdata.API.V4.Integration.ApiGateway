using ApiGateway.Application.Common.Interfaces.Services;
using ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchesWithPagination;

namespace ApiGateway.Infrastructure.ExternalResources.Services;

public sealed class LaunchesService : ILaunchesService
{
    public async Task<IReadOnlyList<PastLaunchesDto>> GetPastLaunchchesAsync(CancellationToken cancellationToken = default)
    {
        return Enumerable.Empty<PastLaunchesDto>().ToList();
    }
}
