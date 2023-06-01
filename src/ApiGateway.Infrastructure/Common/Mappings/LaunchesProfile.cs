using ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchesWithPagination;
using ApiGateway.Application.Features.Launches.Upcoming.Queries.GetUpcomingLaunchesWithPagination;
using ApiGateway.Infrastructure.ExternalResources.Models;
using AutoMapper;

namespace ApiGateway.Infrastructure.Common.Mappings;

public sealed class LaunchesProfile : Profile
{
    public LaunchesProfile()
    {
        MapGetPastLaunchesResponseModelToPastLaunchesDto();
        MapGetUpcomingLaunchesResponseModelToUpcomingLaunchesDto();
    }

    private void MapGetPastLaunchesResponseModelToPastLaunchesDto()
    {
        CreateMap<GetPastLaunchesResponseModel, PastLaunchesDto>()
        .ForMember(dest => dest.PastLaunches, opt => opt.MapFrom(src => src.Docs));

        CreateMap<PastLaunchesResponseWrapper, PastLaunch>()
            .ForMember(dest => dest.Links, opt => opt.MapFrom(src => src.Links))
            .ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => src.Success))
            .ForMember(dest => dest.LaunchedAtUtc, opt => opt.MapFrom(src => src.Date_utc))
            .ForMember(dest => dest.FlightNumber, opt => opt.MapFrom(src => src.Flight_number));

        CreateMap<PastLaunchesResponseLinksWrapper, Medias>()
            .ForMember(dest => dest.SmallImage, opt => opt.MapFrom(src => src.Patch != null ? src.Patch.Small : default))
            .ForMember(dest => dest.LargeImage, opt => opt.MapFrom(src => src.Patch != null ? src.Patch.Large : default))
            .ForMember(dest => dest.Video, opt => opt.MapFrom(src => src.Webcast));

    }

    private void MapGetUpcomingLaunchesResponseModelToUpcomingLaunchesDto()
    {
        CreateMap<GetUpcomingLaunchesResponseModel, UpcomingLaunchesDto>()
        .ForMember(dest => dest.UpcomingLaunches, opt => opt.MapFrom(src => src.Docs));

        CreateMap<UpcomingLaunchesResponseWrapper, UpcomingLaunch>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.LaunchedAtUtc, opt => opt.MapFrom(src => src.Date_utc))
            .ForMember(dest => dest.FlightNumber, opt => opt.MapFrom(src => src.Flight_number));
    }
}
