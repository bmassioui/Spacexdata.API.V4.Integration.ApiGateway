using ApiGateway.WebApi.Availability;
using ApiGateway.WebApi.Configurations;
using ApiGateway.WebApi.Extensions;
using ApiGateway.WebApi.Filters;
using FluentValidation.AspNetCore;
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

        services
            .AddHealthChecks()
            .AddCheck<WebApiHealthCheck>("WebApiHealth");

        services
            .AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());

        services
            .AddFluentValidationClientsideAdapters();

        return services;
    }
}
