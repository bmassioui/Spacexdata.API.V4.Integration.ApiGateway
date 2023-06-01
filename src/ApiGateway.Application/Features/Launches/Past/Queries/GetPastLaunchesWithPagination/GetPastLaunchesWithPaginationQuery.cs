using ApiGateway.Application.Common.Interfaces.Services;
using MediatR;

namespace ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchesWithPagination;

public record class GetPastLaunchesWithPaginationQuery(ushort? Offset, ushort? Limit) : IRequest<PastLaunchesDto> { }

public sealed class GetPastLaunchesWithPaginationQueryHandler : IRequestHandler<GetPastLaunchesWithPaginationQuery, PastLaunchesDto>
{
    private readonly ILaunchesService _launchesService;

    public GetPastLaunchesWithPaginationQueryHandler(ILaunchesService launchesService) => _launchesService = launchesService;

    public async Task<PastLaunchesDto> Handle(GetPastLaunchesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        ushort offset = request.Offset ?? default;
        ushort defaultLimit = 10;
        ushort limit = request.Limit ?? defaultLimit;

        PastLaunchesDto? pastLaunchesDto =
            await _launchesService.GetPastLaunchesAsync(offset, limit, cancellationToken);

        return pastLaunchesDto ?? new();
    }
}
