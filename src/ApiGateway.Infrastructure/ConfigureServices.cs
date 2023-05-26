using ApiGateway.Infrastructure.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiGateway.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        string? spaceXWebApiBaseAddress = configuration.GetValue<string>(Constants.SpaceXWebApiBaseUrlConfigurationKey);

        ArgumentNullException.ThrowIfNull(spaceXWebApiBaseAddress);

        services
            .AddHttpClient(Constants.HttpClientNameForSpaceXWebApi, client =>
            {
                client.BaseAddress = new Uri(spaceXWebApiBaseAddress);
                client.Timeout = new TimeSpan(0, 0, Constants.AddHttpClientTimeOutInSeconds);
            });
            //.AddPolicyHandler()
            //.AddPolicyHandler();

        return services;
    }
}
