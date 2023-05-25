using ApiGateway.WebApi.Extensions;

namespace ApiGateway.WebApi;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddWeApiVersionning()
            .AddWebApiCorsConfigs(configuration);

        return services;
    }
}
