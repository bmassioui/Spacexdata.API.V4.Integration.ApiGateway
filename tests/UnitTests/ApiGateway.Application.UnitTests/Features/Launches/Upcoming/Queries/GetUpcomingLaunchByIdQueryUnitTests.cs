using ApiGateway.Application.Common.Exceptions;
using ApiGateway.Application.Features.Launches.Upcoming.Queries.GetUpcomingLaunchById;

namespace ApiGateway.Application.UnitTests.Features.Launches.Upcoming.Queries;

public class GetUpcomingLaunchByIdQueryUnitTests
{
    private readonly Mock<ILaunchesService> _launchServiceMock;
    private readonly GetUpcomingLaunchByIdQueryHandler _getUpcomingLaunchByIdQueryHandler;

    public GetUpcomingLaunchByIdQueryUnitTests()
    {
        // Initialization
        _launchServiceMock = new Mock<ILaunchesService>();
        _getUpcomingLaunchByIdQueryHandler = new GetUpcomingLaunchByIdQueryHandler(_launchServiceMock.Object);
    }

    [Fact]
    public async Task HandleShouldThrowNotFoundExceptionWhenGetUpcomingLaunchByIdAsyncReturnNullOfUpcomingLaunchByIdDto()
    {
        // Arrange
        string UpcomingLaunchId = string.Empty;

        UpcomingLaunchByIdDto? UpcomingLaunchByIdDto = default;
        GetUpcomingLaunchByIdQuery getUpcomingLaunchByIdQuery = new(UpcomingLaunchId);
        CancellationToken cancellationToken = new();

        _launchServiceMock
            .Setup(service => service.GetUpcomingLaunchByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(UpcomingLaunchByIdDto);

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await _getUpcomingLaunchByIdQueryHandler.Handle(getUpcomingLaunchByIdQuery, cancellationToken));
    }

    [Fact]
    public async Task HandleShouldReturnUpcomingLaunchByIdDto()
    {
        // Arrange
        string UpcomingLaunchId = Guid.NewGuid().ToString();

        UpcomingLaunchByIdDto UpcomingLaunchByIdDto = GetUpcomingLaunchByIdDto();
        GetUpcomingLaunchByIdQuery getUpcomingLaunchByIdQuery = new(UpcomingLaunchId);
        CancellationToken cancellationToken = new();

        _launchServiceMock
            .Setup(service => service.GetUpcomingLaunchByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(UpcomingLaunchByIdDto);

        // Act
        UpcomingLaunchByIdDto? result = await _getUpcomingLaunchByIdQueryHandler.Handle(getUpcomingLaunchByIdQuery, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.True(result == UpcomingLaunchByIdDto);
    }

    private static UpcomingLaunchByIdDto GetUpcomingLaunchByIdDto()
    {
        var UpcomingLaunchByIdDto = new UpcomingLaunchByIdDto
        {
            FlightNumber = 123,
            Name = "Launch 1",
            LaunchedAtUtc = "2023-06-01T12:00:00Z",
            Rocket = "Falcon 9",
            IsSuccess = true,
            Details = "Launch details",
            Links = new UpcomingLaunchByIdMedias
            {
                SmallImage = "small_image_url",
                LargeImage = "large_image_url",
                Video = "video_url"
            },
            AutoUpdate = false
        };

        return UpcomingLaunchByIdDto;
    }
}
