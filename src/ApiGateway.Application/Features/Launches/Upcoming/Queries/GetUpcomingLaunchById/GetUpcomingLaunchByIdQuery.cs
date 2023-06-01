using ApiGateway.Application.Common.Exceptions;
using ApiGateway.Application.Common.Interfaces.Services;
using MediatR;

namespace ApiGateway.Application.Features.Launches.Upcoming.Queries.GetUpcomingLaunchById;

public record class GetUpcomingLaunchByIdQuery(string UpcomingLaunchId) : IRequest<UpcomingLaunchByIdDto> { }

public sealed class GetUpcomingLaunchByIdQueryHandler : IRequestHandler<GetUpcomingLaunchByIdQuery, UpcomingLaunchByIdDto>
{
    private readonly ILaunchesService _launchesService;

    public GetUpcomingLaunchByIdQueryHandler(ILaunchesService launchesService) => _launchesService = launchesService;

    public async Task<UpcomingLaunchByIdDto> Handle(GetUpcomingLaunchByIdQuery request, CancellationToken cancellationToken)
    {
        UpcomingLaunchByIdDto? upcomingLaunchByIdDto =
            await _launchesService.GetUpcomingLaunchByIdAsync(request.UpcomingLaunchId, cancellationToken);

        return
            upcomingLaunchByIdDto is null ?
            throw new NotFoundException(nameof(UpcomingLaunchByIdDto), request.UpcomingLaunchId) :
            upcomingLaunchByIdDto;
    }
}
