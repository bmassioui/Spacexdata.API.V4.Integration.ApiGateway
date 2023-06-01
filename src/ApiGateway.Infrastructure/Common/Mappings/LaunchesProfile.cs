using ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchById;
using ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchesWithPagination;
using ApiGateway.Application.Features.Launches.Upcoming.Queries.GetUpcomingLaunchesWithPagination;
using ApiGateway.Infrastructure.Models;
using AutoMapper;

namespace ApiGateway.Infrastructure.Common.Mappings;

public sealed class LaunchesProfile : Profile
{
    public LaunchesProfile()
    {
        MapGetPastLaunchesResponseModelToPastLaunchesDto();
        MapGetUpcomingLaunchesResponseModelToUpcomingLaunchesDto();
        MapGetPastLaunchResponseModelToPastLaunchByIdDto();
    }

    private void MapGetPastLaunchesResponseModelToPastLaunchesDto()
    {
        CreateMap<GetPastLaunchesResponseModel, PastLaunchesDto>()
        .ForMember(dest => dest.PastLaunches, opt => opt.MapFrom(src => src.Docs))
        .ForMember(dest => dest.TotalPastLaunches, opt => opt.MapFrom(src => src.TotalDocs));

        CreateMap<PastLaunchesResponse, PastLaunch>()
            .ForMember(dest => dest.Links, opt => opt.MapFrom(src => src.Links))
            .ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => src.Success))
            .ForMember(dest => dest.LaunchedAtUtc, opt => opt.MapFrom(src => src.Date_utc))
            .ForMember(dest => dest.FlightNumber, opt => opt.MapFrom(src => src.Flight_number));

        CreateMap<PastLaunchesResponseLinks, PastLaunchMedias>()
            .ForMember<string>(dest => dest.SmallImage, opt => opt.MapFrom<string>((src, dest) => src.Patch?.Small))
            .ForMember<string>(dest => dest.LargeImage, opt => opt.MapFrom<string>((src, dest) => src.Patch?.Large))
            .ForMember<string>(dest => dest.Video, opt => opt.MapFrom<string>((src, dest) => src.Webcast));

    }

    private void MapGetUpcomingLaunchesResponseModelToUpcomingLaunchesDto()
    {
        CreateMap<GetUpcomingLaunchesResponseModel, UpcomingLaunchesDto>()
        .ForMember(dest => dest.UpcomingLaunches, opt => opt.MapFrom(src => src.Docs))
        .ForMember(dest => dest.TotalUpcomingLaunches, opt => opt.MapFrom(src => src.TotalDocs));

        CreateMap<UpcomingLaunchesResponse, UpcomingLaunch>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.LaunchedAtUtc, opt => opt.MapFrom(src => src.Date_utc))
            .ForMember(dest => dest.FlightNumber, opt => opt.MapFrom(src => src.Flight_number));
    }

    private void MapGetPastLaunchResponseModelToPastLaunchByIdDto()
    {
        CreateMap<GetPastLaunchResponseModel, PastLaunchByIdDto>()
           .ForMember(dest => dest.FlightNumber, opt => opt.MapFrom(src => src.FlightNumber))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.LaunchedAtUtc, opt => opt.MapFrom(src => src.Date_utc))
           .ForMember(dest => dest.Rocket, opt => opt.MapFrom(src => src.Rocket))
           .ForMember(dest => dest.Rocket, opt => opt.MapFrom(src => src.Rocket))
           .ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => src.Success))
           .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details))
           .ForMember(dest => dest.AutoUpdate, opt => opt.MapFrom(src => src.AutoUpdate));

        CreateMap<PastLaunchResponseLinks, PastLaunchByIdMedias>()
           .ForMember(dest => dest.SmallImage, opt => opt.MapFrom((src, dest) => src.Patch?.Small))
           .ForMember(dest => dest.LargeImage, opt => opt.MapFrom((src, dest) => src.Patch?.Large))
           .ForMember(dest => dest.Video, opt => opt.MapFrom(src => src.Webcast));
    }
}
