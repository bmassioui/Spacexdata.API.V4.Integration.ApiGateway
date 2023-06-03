using ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchById;
using ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchesWithPagination;
using ApiGateway.Application.Features.Launches.Upcoming.Queries.GetUpcomingLaunchById;
using ApiGateway.Application.Features.Launches.Upcoming.Queries.GetUpcomingLaunchesWithPagination;
using ApiGateway.Infrastructure.Common;
using ApiGateway.Infrastructure.Models;
using ApiGateway.Infrastructure.Options;
using ApiGateway.Infrastructure.Services;
using AutoMapper;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;

namespace ApiGateway.Infrastructure.UnitTests.Services;

public class LaunchesServiceUnitTests
{
    private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly Mock<IOptions<SpaceXWebApiOptions>> _optionsMock;
    private readonly IMapper _mapper;

    public LaunchesServiceUnitTests()
    {
        // Initialization
        _httpClientFactoryMock = new Mock<IHttpClientFactory>();
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        _optionsMock = new Mock<IOptions<SpaceXWebApiOptions>>();
        _mapper = ConfigureAutoMapper().CreateMapper();

        // Setup
        _optionsMock.Setup(x => x.Value).Returns(new SpaceXWebApiOptions { Launches = new LaunchesOptions { PostQueryEndPointUri = "https://example.com/v4/api/launches" } });
    }

    [Fact]
    public async Task GetPastLaunchesAsyncShouldReturnPastLaunchesDto()
    {
        // Arrange
        ushort offset = 0;
        ushort limit = 10;

        GetPastLaunchesResponseModel fakeData = GetFakePastLaunches();
        fakeData.Docs = fakeData.Docs[offset..limit];

        string postRequestExpectedResponse = JsonConvert.SerializeObject(fakeData, Formatting.Indented);
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(postRequestExpectedResponse)
            });

        var httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        _httpClientFactoryMock.Setup(x => x.CreateClient(Constants.HttpClientNameForSpaceXWebApi)).Returns(httpClient);

        var launchesService = new LaunchesService(_httpClientFactoryMock.Object, _optionsMock.Object, _mapper);

        //// Act
        var result = await launchesService.GetPastLaunchesAsync(offset, limit);

        //// Assert
        Assert.NotNull(result);
        Assert.True(result.PastLaunches.Length == limit);
    }

    [Fact]
    public async Task GetPastLaunchesAsyncShouldReturnNullWhenReponseContentIsNullOrEmpty()
    {
        // Arrange
        ushort offset = 0;
        ushort limit = 10;

        GetPastLaunchesResponseModel fakeData = GetFakePastLaunches();
        fakeData.Docs = fakeData.Docs[offset..limit];

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
            });

        var httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        _httpClientFactoryMock.Setup(x => x.CreateClient(Constants.HttpClientNameForSpaceXWebApi)).Returns(httpClient);

        var launchesService = new LaunchesService(_httpClientFactoryMock.Object, _optionsMock.Object, _mapper);

        //// Act
        var result = await launchesService.GetPastLaunchesAsync(offset, limit);

        //// Assert
        Assert.Null(result);
    }

    private static GetPastLaunchesResponseModel GetFakePastLaunches()
    {
        GetPastLaunchesResponseModel fakeData = new()
        {
            Docs = new[]
            {
                new PastLaunchesResponse
                {
                    Links = new PastLaunchesResponseLinks
                    {
                        Patch = new PastLaunchesResponseLinksPatch
                        {
                            Small = "http://example.com/small12345",
                            Large = "http://example.com/large12345"
                        },
                        Webcast = "http://example.com/webcast12345"
                    },
                    Success = true,
                    Details = "Launch successful",
                    Flight_number = 1,
                    Name = "Mission 1",
                    Date_utc = DateTime.UtcNow,
                    Id = "12345"
                },
                new PastLaunchesResponse
                {
                    Links = new PastLaunchesResponseLinks
                    {
                        Patch = new PastLaunchesResponseLinksPatch
                        {
                            Small = "http://example.com/small67892",
                            Large = "http://example.com/large67892"
                        },
                        Webcast = "http://example.com/webcast67892"
                    },
                    Success = false,
                    Details = "Launch failed",
                    Flight_number = 2,
                    Name = "Mission 2",
                    Date_utc = DateTime.UtcNow,
                    Id = "67892"
                },
                new PastLaunchesResponse
                {
                    Links = new PastLaunchesResponseLinks
                    {
                        Patch = new PastLaunchesResponseLinksPatch
                        {
                            Small = "http://example.com/small67893",
                            Large = "http://example.com/large67893"
                        },
                        Webcast = "http://example.com/webcast67893"
                    },
                    Success = false,
                    Details = "Launch failed",
                    Flight_number = 3,
                    Name = "Mission 3",
                    Date_utc = DateTime.UtcNow,
                    Id = "67890"
                },
                new PastLaunchesResponse
                {
                    Links = new PastLaunchesResponseLinks
                    {
                        Patch = new PastLaunchesResponseLinksPatch
                        {
                            Small = "http://example.com/small67894",
                            Large = "http://example.com/large67894"
                        },
                        Webcast = "http://example.com/webcast67894"
                    },
                    Success = true,
                    Details = "Launch succeeded",
                    Flight_number = 4,
                    Name = "Mission 24",
                    Date_utc = DateTime.UtcNow,
                    Id = "67890"
                },
                new PastLaunchesResponse
                {
                    Links = new PastLaunchesResponseLinks
                    {
                        Patch = new PastLaunchesResponseLinksPatch
                        {
                            Small = "http://example.com/small67895",
                            Large = "http://example.com/large67895"
                        },
                        Webcast = "http://example.com/webcast67895"
                    },
                    Success = false,
                    Details = "Launch failed",
                    Flight_number = 5,
                    Name = "Mission 5",
                    Date_utc = DateTime.UtcNow,
                    Id = "67890"
                },
                new PastLaunchesResponse
                {
                    Links = new PastLaunchesResponseLinks
                    {
                        Patch = new PastLaunchesResponseLinksPatch
                        {
                            Small = "http://example.com/small67896",
                            Large = "http://example.com/large67896"
                        },
                        Webcast = "http://example.com/webcast67896"
                    },
                    Success = false,
                    Details = "Launch failed",
                    Flight_number = 6,
                    Name = "Mission 6",
                    Date_utc = DateTime.UtcNow,
                    Id = "67896"
                },
                new PastLaunchesResponse
                {
                    Links = new PastLaunchesResponseLinks
                    {
                        Patch = new PastLaunchesResponseLinksPatch
                        {
                            Small = "http://example.com/small67897",
                            Large = "http://example.com/large67897"
                        },
                        Webcast = "http://example.com/webcast67897"
                    },
                    Success = true,
                    Details = "Launch succeeded",
                    Flight_number = 7,
                    Name = "Mission 7",
                    Date_utc = DateTime.UtcNow,
                    Id = "67897"
                },
                new PastLaunchesResponse
                {
                    Links = new PastLaunchesResponseLinks
                    {
                        Patch = new PastLaunchesResponseLinksPatch
                        {
                            Small = "http://example.com/small67898",
                            Large = "http://example.com/large67898"
                        },
                        Webcast = "http://example.com/webcast67898"
                    },
                    Success = true,
                    Details = "Launch succeeded",
                    Flight_number = 8,
                    Name = "Mission 8",
                    Date_utc = DateTime.UtcNow,
                    Id = "67898"
                },
                new PastLaunchesResponse
                {
                    Links = new PastLaunchesResponseLinks
                    {
                        Patch = new PastLaunchesResponseLinksPatch
                        {
                            Small = "http://example.com/small67899",
                            Large = "http://example.com/large67899"
                        },
                        Webcast = "http://example.com/webcast67899"
                    },
                    Success = true,
                    Details = "Launch succeeded",
                    Flight_number = 9,
                    Name = "Mission 9",
                    Date_utc = DateTime.UtcNow,
                    Id = "67899"
                },
                new PastLaunchesResponse
                {
                    Links = new PastLaunchesResponseLinks
                    {
                        Patch = new PastLaunchesResponseLinksPatch
                        {
                            Small = "http://example.com/small67900",
                            Large = "http://example.com/large67900"
                        },
                        Webcast = "http://example.com/webcast67900"
                    },
                    Success = false,
                    Details = "Launch failed",
                    Flight_number = 10,
                    Name = "Mission 10",
                    Date_utc = DateTime.UtcNow,
                    Id = "67900"
                },
                new PastLaunchesResponse
                {
                    Links = new PastLaunchesResponseLinks
                    {
                        Patch = new PastLaunchesResponseLinksPatch
                        {
                            Small = "http://example.com/small67901",
                            Large = "http://example.com/large67901"
                        },
                        Webcast = "http://example.com/webcast67901"
                    },
                    Success = false,
                    Details = "Launch failed",
                    Flight_number = 11,
                    Name = "Mission 11",
                    Date_utc = DateTime.UtcNow,
                    Id = "67901"
                },
                new PastLaunchesResponse
                {
                    Links = new PastLaunchesResponseLinks
                    {
                        Patch = new PastLaunchesResponseLinksPatch
                        {
                            Small = "http://example.com/small67902",
                            Large = "http://example.com/large67902"
                        },
                        Webcast = "http://example.com/webcast67902"
                    },
                    Success = false,
                    Details = "Launch failed",
                    Flight_number = 12,
                    Name = "Mission 12",
                    Date_utc = DateTime.UtcNow,
                    Id = "67900"
                },
                new PastLaunchesResponse
                {
                    Links = new PastLaunchesResponseLinks
                    {
                        Patch = new PastLaunchesResponseLinksPatch
                        {
                            Small = "http://example.com/small67903",
                            Large = "http://example.com/large67903"
                        },
                        Webcast = "http://example.com/webcast67903"
                    },
                    Success = false,
                    Details = "Launch failed",
                    Flight_number = 13,
                    Name = "Mission 13",
                    Date_utc = DateTime.UtcNow,
                    Id = "67900"
                },
            },
            TotalDocs = 10,
            Offset = 0,
            Limit = 10,
            TotalPages = 1,
            PagingCounter = 1,
            HasPrevPage = false,
            HasNextPage = false,
            PrevPage = null,
            NextPage = null
        };

        return fakeData;
    }

    private static MapperConfiguration ConfigureAutoMapper()
    {
        var configuration = new MapperConfiguration(mapperConfiguration =>
        {
            MapGetPastLaunchesResponseModelToPastLaunchesDto(mapperConfiguration);
            MapGetUpcomingLaunchesResponseModelToUpcomingLaunchesDto(mapperConfiguration);
            MapGetPastLaunchResponseModelToPastLaunchByIdDto(mapperConfiguration);
            MapGetUpcomingLaunchResponseModelToPastLaunchByIdDto(mapperConfiguration);
        });

        return configuration;

        static void MapGetPastLaunchesResponseModelToPastLaunchesDto(IMapperConfigurationExpression mapperConfiguration)
        {
            mapperConfiguration.CreateMap<GetPastLaunchesResponseModel, PastLaunchesDto>()
            .ForMember(dest => dest.PastLaunches, opt => opt.MapFrom(src => src.Docs))
            .ForMember(dest => dest.TotalPastLaunches, opt => opt.MapFrom(src => src.TotalDocs));

            mapperConfiguration.CreateMap<PastLaunchesResponse, PastLaunch>()
                .ForMember(dest => dest.Links, opt => opt.MapFrom(src => src.Links))
                .ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => src.Success))
                .ForMember(dest => dest.LaunchedAtUtc, opt => opt.MapFrom(src => src.Date_utc))
                .ForMember(dest => dest.FlightNumber, opt => opt.MapFrom(src => src.Flight_number));

            mapperConfiguration.CreateMap<PastLaunchesResponseLinks, PastLaunchMedias>()
               .ForMember(dest => dest.SmallImage, opt => opt.MapFrom((src, dest) => src.Patch?.Small))
               .ForMember(dest => dest.LargeImage, opt => opt.MapFrom((src, dest) => src.Patch?.Large))
               .ForMember(dest => dest.Video, opt => opt.MapFrom(src => src.Webcast));
        }

        static void MapGetUpcomingLaunchesResponseModelToUpcomingLaunchesDto(IMapperConfigurationExpression mapperConfiguration)
        {
            mapperConfiguration.CreateMap<GetUpcomingLaunchesResponseModel, UpcomingLaunchesDto>()
            .ForMember(dest => dest.UpcomingLaunches, opt => opt.MapFrom(src => src.Docs))
            .ForMember(dest => dest.TotalUpcomingLaunches, opt => opt.MapFrom(src => src.TotalDocs));

            mapperConfiguration.CreateMap<UpcomingLaunchesResponse, UpcomingLaunch>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LaunchedAtUtc, opt => opt.MapFrom(src => src.Date_utc))
                .ForMember(dest => dest.FlightNumber, opt => opt.MapFrom(src => src.Flight_number));
        }

        static void MapGetPastLaunchResponseModelToPastLaunchByIdDto(IMapperConfigurationExpression mapperConfiguration)
        {
            mapperConfiguration.CreateMap<GetPastLaunchResponseModel, PastLaunchByIdDto>()
               .ForMember(dest => dest.FlightNumber, opt => opt.MapFrom(src => src.FlightNumber))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.LaunchedAtUtc, opt => opt.MapFrom(src => src.Date_utc))
               .ForMember(dest => dest.Rocket, opt => opt.MapFrom(src => src.Rocket))
               .ForMember(dest => dest.Rocket, opt => opt.MapFrom(src => src.Rocket))
               .ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => src.Success))
               .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details))
               .ForMember(dest => dest.AutoUpdate, opt => opt.MapFrom(src => src.AutoUpdate));

            mapperConfiguration.CreateMap<PastLaunchResponseLinks, PastLaunchByIdMedias>()
               .ForMember(dest => dest.SmallImage, opt => opt.MapFrom((src, dest) => src.Patch?.Small))
               .ForMember(dest => dest.LargeImage, opt => opt.MapFrom((src, dest) => src.Patch?.Large))
               .ForMember(dest => dest.Video, opt => opt.MapFrom(src => src.Webcast));
        }

        static void MapGetUpcomingLaunchResponseModelToPastLaunchByIdDto(IMapperConfigurationExpression mapperConfiguration)
        {
            mapperConfiguration.CreateMap<GetUpcomingLaunchResponseModel, UpcomingLaunchByIdDto>()
               .ForMember(dest => dest.FlightNumber, opt => opt.MapFrom(src => src.FlightNumber))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.LaunchedAtUtc, opt => opt.MapFrom(src => src.Date_utc))
               .ForMember(dest => dest.Rocket, opt => opt.MapFrom(src => src.Rocket))
               .ForMember(dest => dest.Rocket, opt => opt.MapFrom(src => src.Rocket))
               .ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => src.Success))
               .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details))
               .ForMember(dest => dest.AutoUpdate, opt => opt.MapFrom(src => src.AutoUpdate));

            mapperConfiguration.CreateMap<UpcomingLaunchResponseLinks, UpcomingLaunchByIdMedias>()
               .ForMember(dest => dest.SmallImage, opt => opt.MapFrom((src, dest) => src.Patch?.Small))
               .ForMember(dest => dest.LargeImage, opt => opt.MapFrom((src, dest) => src.Patch?.Large))
               .ForMember(dest => dest.Video, opt => opt.MapFrom(src => src.Webcast));
        }

    }
}
