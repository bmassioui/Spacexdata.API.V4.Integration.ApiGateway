using ApiGateway.Application.Common.Interfaces.Services;
using ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchesWithPagination;
using ApiGateway.Infrastructure.Common;
using ApiGateway.Infrastructure.ExternalResources.Models;
using ApiGateway.Infrastructure.Options;
using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace ApiGateway.Infrastructure.ExternalResources.Services;

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

    public async Task<PastLaunchesDto?> GetPastLaunchchesAsync(ushort offset, ushort limit,CancellationToken cancellationToken = default)
    {
        GetPastLaunchesRequestModel payload = GetGetPastLaunchchesRequestPayload(offset, limit);

        var postResponse = await _httpClient.PostAsJsonAsync(_spaceXWebApiOptions.Launches.PostQueryEndPointUri, payload, cancellationToken);

        if (postResponse is null) return default;

        if (!postResponse.IsSuccessStatusCode) postResponse.EnsureSuccessStatusCode();

        string? postResponseAsString = await postResponse.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(postResponseAsString)) return default;

        GetPastLaunchesResponseModel? pastLaunchesResponseModel =
            JsonConvert.DeserializeObject<GetPastLaunchesResponseModel>(postResponseAsString);

        if(pastLaunchesResponseModel is null) return default;

        PastLaunchesDto pastLaunchesDto = 
            _mapper.Map<GetPastLaunchesResponseModel, PastLaunchesDto>(pastLaunchesResponseModel);

        return pastLaunchesDto;

        static GetPastLaunchesRequestModel GetGetPastLaunchchesRequestPayload(ushort offset, ushort limit)
        {
            var defaultSelection = new string[] { "id", "flight_number", "name", "success", "details", "date_utc", "links.patch", "links.webcast" };
            Sort defaultSortingBy = new() { Date_Utc = "desc" };
            GetPastLaunchesRequestModel payload = new()
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
}