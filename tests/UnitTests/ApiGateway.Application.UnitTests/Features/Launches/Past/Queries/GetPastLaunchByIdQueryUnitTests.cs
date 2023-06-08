using ApiGateway.Application.Common.Exceptions;
using ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchById;

namespace ApiGateway.Application.UnitTests.Features.Launches.Past.Queries;

public class GetPastLaunchByIdQueryUnitTests
{
    private readonly Mock<ILaunchesService> _launchServiceMock;
    private readonly GetPastLaunchByIdQueryHandler _getPastLaunchByIdQueryHandler;

    public GetPastLaunchByIdQueryUnitTests()
    {
        // Initialization
        _launchServiceMock = new Mock<ILaunchesService>();
        _getPastLaunchByIdQueryHandler = new GetPastLaunchByIdQueryHandler(_launchServiceMock.Object);
    }

    [Fact]
    public async Task HandleShouldThrowNotFoundExceptionWhenGetPastLaunchByIdAsyncReturnNullOfPastLaunchByIdDto()
    {
        // Arrange
        string pastLaunchId = string.Empty;

        PastLaunchByIdDto? pastLaunchByIdDto = default;
        GetPastLaunchByIdQuery getPastLaunchByIdQuery = new(pastLaunchId);
        CancellationToken cancellationToken = new();

        _launchServiceMock
            .Setup(service => service.GetPastLaunchByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(pastLaunchByIdDto);

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await _getPastLaunchByIdQueryHandler.Handle(getPastLaunchByIdQuery, cancellationToken));
    }

    [Fact]
    public async Task HandleShouldReturnPastLaunchByIdDto()
    {
        // Arrange
        string pastLaunchId = Guid.NewGuid().ToString();

        PastLaunchByIdDto pastLaunchByIdDto = GetPastLaunchByIdDto();
        GetPastLaunchByIdQuery getPastLaunchByIdQuery = new(pastLaunchId);
        CancellationToken cancellationToken = new();

        _launchServiceMock
            .Setup(service => service.GetPastLaunchByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(pastLaunchByIdDto);

        // Act
        PastLaunchByIdDto? result = await _getPastLaunchByIdQueryHandler.Handle(getPastLaunchByIdQuery, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.True(result == pastLaunchByIdDto);
    }

    private static PastLaunchByIdDto GetPastLaunchByIdDto()
    {
        var pastLaunchByIdDto = new PastLaunchByIdDto
        {
            FlightNumber = 123,
            Name = "Launch 1",
            LaunchedAtUtc = "2023-06-01T12:00:00Z",
            Rocket = "Falcon 9",
            IsSuccess = true,
            Details = "Launch details",
            Links = new PastLaunchByIdMedias
            {
                SmallImage = "small_image_url",
                LargeImage = "large_image_url",
                Video = "video_url"
            },
            AutoUpdate = false
        };

        return pastLaunchByIdDto;
    }
}
