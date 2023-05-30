using ApiGateway.Application.Common.Interfaces.Services;
using ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchesWithPagination;
using ApiGateway.Application.Options;
using ApiGateway.Infrastructure.Common;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using Newtonsoft.Json;

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
        var payload = new Rootobject
        {
            options = new Options
            {
                offset = 0,
                limit = 10,
                sort = new Sort { date_utc = "desc" },
                select = new string[] { "id", "flight_number", "name", "success", "details", "date_utc", "links.patch", "links.webcast" }
            }
        };

        var postResponse = await _httpClient.PostAsJsonAsync(_spaceXWebApiOptions.Launches.PostQueryEndPointUri, payload, cancellationToken);

        if (postResponse is null) return default;

        if (!postResponse.IsSuccessStatusCode) postResponse.EnsureSuccessStatusCode();

        string? postResponseAsString = await postResponse.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(postResponseAsString)) return default;

        PastLaunchesDto? pastLaunchesDto =
            JsonConvert.DeserializeObject<PastLaunchesDto>(postResponseAsString);

        return pastLaunchesDto;
    }
}

public class Rootobject
{
    public Options options { get; set; }
}

public class Options
{
    public int offset { get; set; }
    public int limit { get; set; }
    public Sort sort { get; set; }
    public string[] select { get; set; }
}

public class Sort
{
    public string date_utc { get; set; }
}
