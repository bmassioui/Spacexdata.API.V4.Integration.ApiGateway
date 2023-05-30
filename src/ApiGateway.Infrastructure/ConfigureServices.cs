using ApiGateway.Infrastructure.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly.Extensions.Http;
using Polly;
using System.Diagnostics.CodeAnalysis;
using ApiGateway.Application.Common.Interfaces.Services;
using ApiGateway.Infrastructure.ExternalResources.Services;

namespace ApiGateway.Infrastructure;

[ExcludeFromCodeCoverage]
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
                client.Timeout = new TimeSpan(default, default, Constants.AddHttpClientTimeOutInSeconds);
            })
        .AddPolicyHandler(GetRetryPolicy())
        .AddPolicyHandler(GetCircuitBreakerPolicy());

        services.AddTransient<ILaunchesService, LaunchesService>();

        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() =>
        HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(Constants.RetryPolicyCount, _ => TimeSpan.FromSeconds(Constants.RetryPolicySleepDuration));

    private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy() =>
         HttpPolicyExtensions
        .HandleTransientHttpError()
        .CircuitBreakerAsync(Constants.CircuitBreakerPolicyNumberOfAllowedFailedAttemptsBeforeBreaking, TimeSpan.FromSeconds(Constants.CircuitBreakerPolicyDurationOfBreak));
}
