using ApiGateway.Infrastructure.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
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

        SetLogger(configurationBuilder, environment);

        return builder.UseSerilog();
    }

    private static void SetLogger(IConfigurationRoot? configuration, string environment)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        Log.Logger =
            new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentName()
            .Enrich.WithThreadId()
            .Enrich.WithThreadId()
            .Enrich.WithProcessId()
            .Enrich.WithProcessName()
            .Enrich.WithProperty("Environment", environment)
            .WriteTo.Console()
            .WriteTo.Debug()
            .WriteTo.Elasticsearch(CreateElasticsearchSinkOptions(configuration))
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }

    private static ElasticsearchSinkOptions CreateElasticsearchSinkOptions(IConfigurationRoot? configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        string? nodeUri = configuration[Constants.ElasticsearchNodeUriConfigurationKey];

        ArgumentException.ThrowIfNullOrEmpty(nodeUri);

        return new ElasticsearchSinkOptions(new Uri(nodeUri))
        {
            AutoRegisterTemplate = true,
            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv8,
            NumberOfReplicas = 5,
            IndexFormat = Constants.ElasticsearchIndexFormat,
        };
    }
}
