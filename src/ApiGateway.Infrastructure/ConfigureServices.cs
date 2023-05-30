using ApiGateway.Application.Common.Interfaces.Services;
using ApiGateway.Infrastructure.Common;
using ApiGateway.Infrastructure.ExternalResources.Services;
using ApiGateway.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using AutoMapper;
using System.Reflection;

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
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
        .AddPolicyHandler(GetRetryPolicy())
        .AddPolicyHandler(GetCircuitBreakerPolicy());

        services.AddTransient<ILaunchesService, LaunchesService>();

        services.Configure<SpaceXWebApiOptions>(configuration.GetSection(Constants.SpaceXWebApiConfigurationKey));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

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
