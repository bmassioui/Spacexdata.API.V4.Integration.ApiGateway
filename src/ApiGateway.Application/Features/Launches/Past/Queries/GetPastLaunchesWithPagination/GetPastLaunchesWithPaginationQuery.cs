using ApiGateway.Application.Common.Interfaces.Services;
using MediatR;

namespace ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchesWithPagination;

public record class GetPastLaunchesWithPaginationQuery : IRequest<PastLaunchesDto> { }

public sealed class GetPastLaunchesWithPaginationQueryHandler : IRequestHandler<GetPastLaunchesWithPaginationQuery, PastLaunchesDto>
{
    private readonly ILaunchesService _launchesService;

    public GetPastLaunchesWithPaginationQueryHandler(ILaunchesService launchesService) => _launchesService = launchesService;

    public async Task<PastLaunchesDto> Handle(GetPastLaunchesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        PastLaunchesDto? pastLaunchesDto =
            await _launchesService.GetPastLaunchchesAsync(cancellationToken);

        return pastLaunchesDto ?? new();
    }
}
