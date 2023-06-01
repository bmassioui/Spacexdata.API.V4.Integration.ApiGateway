using ApiGateway.Application.Common.Exceptions;
using ApiGateway.Application.Common.Interfaces.Services;
using MediatR;

namespace ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchById;

public record class GetPastLaunchByIdQuery(string PastLaunchId) : IRequest<PastLaunchByIdDto> { }

public sealed class GetPastLaunchByIdQueryHandler : IRequestHandler<GetPastLaunchByIdQuery, PastLaunchByIdDto>
{
    private readonly ILaunchesService _launchesService;

    public GetPastLaunchByIdQueryHandler(ILaunchesService launchesService) => _launchesService = launchesService;

    public async Task<PastLaunchByIdDto> Handle(GetPastLaunchByIdQuery request, CancellationToken cancellationToken)
    {
        PastLaunchByIdDto? pastLaunchByIdDto =
            await _launchesService.GetPastLaunchByIdAsync(request.PastLaunchId, cancellationToken);

        return
            pastLaunchByIdDto is null ?
            throw new NotFoundException(nameof(PastLaunchByIdDto), request.PastLaunchId) :
            pastLaunchByIdDto;
    }
}
