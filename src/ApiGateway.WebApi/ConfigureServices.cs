using ApiGateway.WebApi.Availability;
using ApiGateway.WebApi.Configurations;
using ApiGateway.WebApi.Extensions;
using ApiGateway.WebApi.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Threading.RateLimiting;

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
            .AddCheck<WebApiHealthCheck>(Constants.HealthCheckName);

        services
            .AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());

        services
            .AddFluentValidationClientsideAdapters();

        services
            .AddOutputCache(options =>
        {
            options.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(10);
        });

        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.OnRejected = async (context, cancellationToken) =>
            {
                var hostName = context.HttpContext.Request.Headers.Host.ToString();

                var message = $"{Constants.FixedWindowLimiterForRejectedRequest} {hostName}";

                await context.HttpContext.Response.WriteAsync(message, cancellationToken);
            };
            options.AddFixedWindowLimiter(policyName: Constants.FixedWindowLimiterPolicyName, options =>
            {
                options.Window = TimeSpan.FromSeconds(5);
                options.PermitLimit = 5;
                options.QueueLimit = 10;
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            });
        }
        );

        return services;
    }
}
