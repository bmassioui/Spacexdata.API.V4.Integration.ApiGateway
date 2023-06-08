using ApiGateway.Application.Common.Interfaces.Services;
using ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchById;
using ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchesWithPagination;
using ApiGateway.Application.Features.Launches.Upcoming.Queries.GetUpcomingLaunchById;
using ApiGateway.Application.Features.Launches.Upcoming.Queries.GetUpcomingLaunchesWithPagination;
using ApiGateway.Infrastructure.Common;
using ApiGateway.Infrastructure.Models;
using ApiGateway.Infrastructure.Options;
using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace ApiGateway.Infrastructure.Services;

public sealed class LaunchesService : ILaunchesService
{
    private readonly HttpClient _httpClient;
    private readonly SpaceXWebApiOptions _spaceXWebApiOptions;
    private readonly IMapper _mapper;

    public LaunchesService(
        IHttpClientFactory httpClientFactory,
        IOptions<SpaceXWebApiOptions> spaceXWebApiOptions,
        IMapper mapper)
    {
        _httpClient = httpClientFactory.CreateClient(Constants.HttpClientNameForSpaceXWebApi);
        _spaceXWebApiOptions = spaceXWebApiOptions.Value;
        _mapper = mapper;
    }

    public async Task<PastLaunchesDto?> GetPastLaunchesAsync(ushort offset, ushort limit, CancellationToken cancellationToken = default)
    {
        GetPastLaunchesRequestModel payload = GetGetPastLaunchchesRequestPayload(offset, limit);

        var postResponse = await _httpClient.PostAsJsonAsync(_spaceXWebApiOptions.Launches.PostQueryEndPointUri, payload, cancellationToken);

        if (postResponse is null) return default;

        if (!postResponse.IsSuccessStatusCode) postResponse.EnsureSuccessStatusCode();

        string? postResponseAsString = await postResponse.Content.ReadAsStringAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(postResponseAsString)) return default;

        GetPastLaunchesResponseModel? pastLaunchesResponseModel =
            JsonConvert.DeserializeObject<GetPastLaunchesResponseModel>(postResponseAsString);

        if (pastLaunchesResponseModel is null) return default;

        PastLaunchesDto pastLaunchesDto =
            _mapper.Map<GetPastLaunchesResponseModel, PastLaunchesDto>(pastLaunchesResponseModel);

        return pastLaunchesDto;

        static GetPastLaunchesRequestModel GetGetPastLaunchchesRequestPayload(ushort offset, ushort limit)
        {
            var defaultSelection = new string[] { "id", "flight_number", "name", "success", "details", "date_utc", "links.patch", "links.webcast" };
            PastLaunchesRequestSortOptions defaultSortingBy = new() { Date_Utc = "desc" };
            GetPastLaunchesRequestModel payload = new()
            {
                Query = new()
                {
                    Upcoming = false
                },
                Options = new()
                {
                    Offset = offset,
                    Limit = limit,
                    Sort = defaultSortingBy,
                    Select = defaultSelection
                }
            };

            return payload;
        }
    }

    public async Task<PastLaunchByIdDto?> GetPastLaunchByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(id);

        var getByIdRequestUri = $"{_spaceXWebApiOptions.Launches.GetByIdEndPointUri}/{id}";
        var getResponse = await _httpClient.GetAsync(getByIdRequestUri, cancellationToken);

        if (getResponse is null) return default;

        if (!getResponse.IsSuccessStatusCode) getResponse.EnsureSuccessStatusCode();

        string? getResponseAsString = await getResponse.Content.ReadAsStringAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(getResponseAsString)) return default;

        GetPastLaunchResponseModel? pastLaunchResponseModel =
           JsonConvert.DeserializeObject<GetPastLaunchResponseModel>(getResponseAsString);

        if (pastLaunchResponseModel is null) return default;

        PastLaunchByIdDto pastLaunchDto =
            _mapper.Map<GetPastLaunchResponseModel, PastLaunchByIdDto>(pastLaunchResponseModel);

        return pastLaunchDto;
    }

    public async Task<UpcomingLaunchesDto?> GetUpcomingLaunchesAsync(ushort offset, ushort limit, CancellationToken cancellationToken = default)
    {
        GetUpcomingLaunchesRequestModel payload = GetGetUpcomingLaunchchesRequestPayload(offset, limit);

        var postResponse = await _httpClient.PostAsJsonAsync(_spaceXWebApiOptions.Launches.PostQueryEndPointUri, payload, cancellationToken);

        if (postResponse is null) return default;

        if (!postResponse.IsSuccessStatusCode) postResponse.EnsureSuccessStatusCode();

        string? postResponseAsString = await postResponse.Content.ReadAsStringAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(postResponseAsString)) return default;

        GetUpcomingLaunchesResponseModel? upcomingLaunchesResponseModel =
            JsonConvert.DeserializeObject<GetUpcomingLaunchesResponseModel>(postResponseAsString);

        if (upcomingLaunchesResponseModel is null) return default;

        UpcomingLaunchesDto upcomingLaunchesDto =
            _mapper.Map<GetUpcomingLaunchesResponseModel, UpcomingLaunchesDto>(upcomingLaunchesResponseModel);

        return upcomingLaunchesDto;

        static GetUpcomingLaunchesRequestModel GetGetUpcomingLaunchchesRequestPayload(ushort offset, ushort limit)
        {
            var defaultSelection = new string[] { "id", "flight_number", "name", "success", "details", "date_utc", "links.patch", "links.webcast" };
            UpcomingLaunchesRequestSort defaultSortingBy = new() { Date_Utc = "desc" };
            GetUpcomingLaunchesRequestModel payload = new()
            {
                Options = new()
                {
                    Offset = offset,
                    Limit = limit,
                    Sort = defaultSortingBy,
                    Select = defaultSelection
                }
            };

            return payload;
        }
    }

    public async Task<UpcomingLaunchByIdDto?> GetUpcomingLaunchByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(id);

        var getByIdRequestUri = $"{_spaceXWebApiOptions.Launches.GetByIdEndPointUri}/{id}";
        var getResponse = await _httpClient.GetAsync(getByIdRequestUri, cancellationToken);

        if (getResponse is null) return default;

        if (!getResponse.IsSuccessStatusCode) getResponse.EnsureSuccessStatusCode();

        string? getResponseAsString = await getResponse.Content.ReadAsStringAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(getResponseAsString)) return default;

        GetUpcomingLaunchResponseModel? upcomingLaunchResponseModel =
           JsonConvert.DeserializeObject<GetUpcomingLaunchResponseModel>(getResponseAsString);

        if (upcomingLaunchResponseModel is null) return default;

        UpcomingLaunchByIdDto upcomingLaunchDto =
            _mapper.Map<GetUpcomingLaunchResponseModel, UpcomingLaunchByIdDto>(upcomingLaunchResponseModel);

        return upcomingLaunchDto;
    }
}

