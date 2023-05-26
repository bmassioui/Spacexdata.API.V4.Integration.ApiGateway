using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ApiGateway.WebApi.Availability;

public class WebApiHealthCheck : IHealthCheck
{
    private readonly Random _random = new();

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        int responseTime = _random.Next(1, 300);

        return responseTime switch
        {
            < 100 => Task.FromResult(HealthCheckResult.Healthy("ApiGateway is healthy ✅.")),
            < 200 => Task.FromResult(HealthCheckResult.Degraded("ApiGateway is degraded ⚠️.")),
            _ => Task.FromResult(HealthCheckResult.Unhealthy("ApiGateway is unhealthy ❌.")),
        };
    }
}