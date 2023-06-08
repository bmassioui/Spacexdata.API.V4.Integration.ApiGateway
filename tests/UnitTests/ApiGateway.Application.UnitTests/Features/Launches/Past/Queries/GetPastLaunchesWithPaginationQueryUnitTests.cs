using ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchesWithPagination;

namespace ApiGateway.Application.UnitTests.Features.Launches.Past.Queries;

public class GetPastLaunchesWithPaginationQueryUnitTests
{
    private readonly Mock<ILaunchesService> _launchServiceMock;
    private readonly GetPastLaunchesWithPaginationQueryHandler _getPastLaunchesWithPaginationQueryHandler;

    public GetPastLaunchesWithPaginationQueryUnitTests()
    {
        // Initialization
        _launchServiceMock = new Mock<ILaunchesService>();
        _getPastLaunchesWithPaginationQueryHandler = new GetPastLaunchesWithPaginationQueryHandler(_launchServiceMock.Object);
    }

    [Fact]
    public async Task HandleShouldReturnEmptyPastLaunchesDtoWhenGetPastLaunchesAsyncReturnsNull()
    {
        // Arrange
        PastLaunchesDto? PastLaunchesDto = default;
        GetPastLaunchesWithPaginationQuery getPastLaunchesWithPaginationQuery = new(default, default);
        CancellationToken cancellationToken = new();

        _launchServiceMock
            .Setup(service => service.GetPastLaunchesAsync(It.IsAny<ushort>(), It.IsAny<ushort>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(PastLaunchesDto);

        PastLaunchesDto expectedResult = new();

        // Act
        PastLaunchesDto? result = await _getPastLaunchesWithPaginationQueryHandler.Handle(getPastLaunchesWithPaginationQuery, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.True(result == expectedResult);
    }

    [Fact]
    public async Task HandleShouldReturnEmptyPastLaunchesDtoWhenGetPastLaunchesAsyncReturnsEmptyPastLaunchesDto()
    {
        // Arrange
        PastLaunchesDto PastLaunchesDto = new();
        GetPastLaunchesWithPaginationQuery getPastLaunchesWithPaginationQuery = new(default, default);
        CancellationToken cancellationToken = new();

        _launchServiceMock
            .Setup(service => service.GetPastLaunchesAsync(It.IsAny<ushort>(), It.IsAny<ushort>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(PastLaunchesDto);

        PastLaunchesDto expectedResult = new();

        // Act
        PastLaunchesDto? result = await _getPastLaunchesWithPaginationQueryHandler.Handle(getPastLaunchesWithPaginationQuery, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.True(result == expectedResult);
    }
}