using ApiGateway.Application.Common.Interfaces.Services;
using MediatR;

namespace ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchesWithPagination;

public record class GetPastLaunchesWithPaginationQuery: IRequest<IReadOnlyList<PastLaunchesDto>>{}

public sealed class GetPastLaunchesWithPaginationQueryHandler : IRequestHandler<GetPastLaunchesWithPaginationQuery, IReadOnlyList<PastLaunchesDto>>
{
    private readonly ILaunchesService _launchesService;

    public GetPastLaunchesWithPaginationQueryHandler(ILaunchesService launchesService) => _launchesService = launchesService;

    public async Task<IReadOnlyList<PastLaunchesDto>> Handle(GetPastLaunchesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<PastLaunchesDto> pastLaunchesDtos =
            await _launchesService.GetPastLaunchchesAsync(cancellationToken);

        return pastLaunchesDtos;
    }
}
