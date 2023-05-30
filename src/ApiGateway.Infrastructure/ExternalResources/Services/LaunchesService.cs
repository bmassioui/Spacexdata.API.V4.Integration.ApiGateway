using ApiGateway.Application.Common.Interfaces.Services;
using ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchesWithPagination;
using ApiGateway.Infrastructure.Common;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using Newtonsoft.Json;
using ApiGateway.Infrastructure.ExternalResources.Models;
using ApiGateway.Infrastructure.Options;

namespace ApiGateway.Infrastructure.ExternalResources.Services;

public sealed class LaunchesService : ILaunchesService
{
    private readonly HttpClient _httpClient;
    private readonly SpaceXWebApiOptions _spaceXWebApiOptions;


    public LaunchesService(IHttpClientFactory httpClientFactory, IOptions<SpaceXWebApiOptions> spaceXWebApiOptions)
    {
        _httpClient = httpClientFactory.CreateClient(Constants.HttpClientNameForSpaceXWebApi);
        _spaceXWebApiOptions = spaceXWebApiOptions.Value;
    }

    public async Task<PastLaunchesDto?> GetPastLaunchchesAsync(CancellationToken cancellationToken = default)
    {
        GetPastLaunchesRequestModel payload = GetGetPastLaunchchesRequestPayload();

        var postResponse = await _httpClient.PostAsJsonAsync(_spaceXWebApiOptions.Launches.PostQueryEndPointUri, payload, cancellationToken);

        if (postResponse is null) return default;

        if (!postResponse.IsSuccessStatusCode) postResponse.EnsureSuccessStatusCode();

        string? postResponseAsString = await postResponse.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(postResponseAsString)) return default;

        PastLaunchesDto? pastLaunchesDto =
            JsonConvert.DeserializeObject<PastLaunchesDto>(postResponseAsString);

        return pastLaunchesDto;

        static GetPastLaunchesRequestModel GetGetPastLaunchchesRequestPayload()
        {
            var defaultSelection = new string[] { "id", "flight_number", "name", "success", "details", "date_utc", "links.patch", "links.webcast" };
            Sort defaultSortingBy = new() { Date_Utc = "desc" };
            GetPastLaunchesRequestModel payload = new()
            {
                Options = new()
                {
                    Offset = 0,
                    Limit = 10,
                    Sort = defaultSortingBy,
                    Select = defaultSelection
                }
            };

            return payload;
        }
    }
}