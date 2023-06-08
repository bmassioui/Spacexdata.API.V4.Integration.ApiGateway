using ApiGateway.Application.Features.Launches.Upcoming.Queries.GetUpcomingLaunchesWithPagination;

namespace ApiGateway.Application.UnitTests.Features.Launches.Upcoming.Queries;

public class GetUpcomingLaunchesWithPaginationQueryUnitTests
{
    private readonly Mock<ILaunchesService> _launchServiceMock;
    private readonly GetUpcomingLaunchesWithPaginationQueryHandler _getUpcomingLaunchesWithPaginationQueryHandler;

    public GetUpcomingLaunchesWithPaginationQueryUnitTests()
    {
        // Initialization
        _launchServiceMock = new Mock<ILaunchesService>();
        _getUpcomingLaunchesWithPaginationQueryHandler = new GetUpcomingLaunchesWithPaginationQueryHandler(_launchServiceMock.Object);
    }

    [Fact]
    public async Task HandleShouldReturnEmptyUpcomingLaunchesDtoWhenGetUpcomingLaunchesAsyncReturnsNull()
    {
        // Arrange
        UpcomingLaunchesDto? upcomingLaunchesDto = default;
        GetUpcomingLaunchesWithPaginationQuery getUpcomingLaunchesWithPaginationQuery = new(default, default);
        CancellationToken cancellationToken = new();

        _launchServiceMock
            .Setup(service => service.GetUpcomingLaunchesAsync(It.IsAny<ushort>(), It.IsAny<ushort>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(upcomingLaunchesDto);

        UpcomingLaunchesDto expectedResult = new();

        // Act
        UpcomingLaunchesDto? result = await _getUpcomingLaunchesWithPaginationQueryHandler.Handle(getUpcomingLaunchesWithPaginationQuery, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.True(result == expectedResult);
    }

    [Fact]
    public async Task HandleShouldReturnEmptyUpcomingLaunchesDtoWhenGetUpcomingLaunchesAsyncReturnsEmptyUpcomingLaunchesDto()
    {
        // Arrange
        UpcomingLaunchesDto upcomingLaunchesDto = new();
        GetUpcomingLaunchesWithPaginationQuery getUpcomingLaunchesWithPaginationQuery = new(default, default);
        CancellationToken cancellationToken = new();

        _launchServiceMock
            .Setup(service => service.GetUpcomingLaunchesAsync(It.IsAny<ushort>(), It.IsAny<ushort>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(upcomingLaunchesDto);

        UpcomingLaunchesDto expectedResult = new();

        // Act
        UpcomingLaunchesDto? result = await _getUpcomingLaunchesWithPaginationQueryHandler.Handle(getUpcomingLaunchesWithPaginationQuery, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.True(result == expectedResult);
    }
}
