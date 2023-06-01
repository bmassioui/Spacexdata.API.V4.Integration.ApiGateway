using ApiGateway.Application.Common.Interfaces.Services;
using MediatR;

namespace ApiGateway.Application.Features.Launches.Upcoming.Queries.GetUpcomingLaunchesWithPagination;

public record class GetUpcomingLaunchesWithPaginationQuery(ushort? Offset, ushort? Limit) : IRequest<UpcomingLaunchesDto> { }

public sealed class GetUpcomingLaunchesWithPaginationQueryHandler : IRequestHandler<GetUpcomingLaunchesWithPaginationQuery, UpcomingLaunchesDto>
{
    private readonly ILaunchesService _launchesService;

    public GetUpcomingLaunchesWithPaginationQueryHandler(ILaunchesService launchesService) => _launchesService = launchesService;

    public async Task<UpcomingLaunchesDto> Handle(GetUpcomingLaunchesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        ushort offset = request.Offset ?? default;
        ushort defaultLimit = 10;
        ushort limit = request.Limit ?? defaultLimit;

        UpcomingLaunchesDto? upcomingLaunchesDto =
            await _launchesService.GetUpcomingLaunchchesAsync(offset, limit, cancellationToken);

        return upcomingLaunchesDto ?? new();
    }
}
