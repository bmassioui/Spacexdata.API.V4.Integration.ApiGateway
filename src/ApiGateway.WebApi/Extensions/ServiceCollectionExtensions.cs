using Microsoft.AspNetCore.Mvc.Versioning;

namespace ApiGateway.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWeApiVersionning(this IServiceCollection services)
    {
        services.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            opt.ReportApiVersions = true;
            opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                            new HeaderApiVersionReader("x-api-version"),
                                                            new MediaTypeApiVersionReader("x-api-version"));
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    public static IServiceCollection AddWebApiCorsConfigs(this IServiceCollection services, IConfiguration configuration)
    {
        var webApiCorsPolicyName =
            configuration.GetValue<string>(Constants.WebApiCorsPolicyNameConfigurationKey) ??
            throw new ArgumentNullException(Constants.WebApiCorsPolicyNameConfigurationKeyNotFoundExceptionMessage);

        services.AddCors(options =>
        {
            options.AddPolicy(name: webApiCorsPolicyName,
                              policy =>
                              {
                                  policy.AllowAnyOrigin() // To limit later
                                  .AllowAnyHeader()
                                  .AllowAnyMethod();
                              });
        });

        return services;
    }
}
