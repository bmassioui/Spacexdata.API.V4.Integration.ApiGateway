using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Diagnostics.CodeAnalysis;

namespace ApiGateway.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class HostBuilderExtensions
{
    public static IHostBuilder ConfigureLogger(this IHostBuilder builder)
    {
        string? environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        ArgumentException.ThrowIfNullOrEmpty(environment);

        var configurationBuilder =
            new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .Build();

        SetLogger(configurationBuilder);

        return builder.UseSerilog();

        static void SetLogger(IConfigurationRoot? configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration);

            Log.Logger =
                new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}
