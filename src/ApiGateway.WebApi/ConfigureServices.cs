using ApiGateway.WebApi.Configurations;
using ApiGateway.WebApi.Extensions;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiGateway.WebApi;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureGenOptions>();

        services
            .AddWeApiVersionning()
            .AddWebApiCorsConfigs(configuration);

        return services;
    }
}
