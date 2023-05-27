using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace ApiGateway.WebApi.Configurations;

public class SwaggerConfigureGenOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public SwaggerConfigureGenOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
        ConfigureSwaggerDocs(options);
        ConfigureSwaggerSecurity(options);
        ConfigureSwaggerXmlComments(options);
    }

    private void ConfigureSwaggerDocs(SwaggerGenOptions options)
    {
        foreach (var desc in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(desc.GroupName, new OpenApiInfo
            {
                Title = $"Spacexdata.API.Integration.ApiGateway {desc.ApiVersion}",
                Description = "Spacexdata Api integration ApiGateway",
                Version = desc.ApiVersion.ToString(),
                Contact = new OpenApiContact
                {
                    Name = "Bouchaib MASSIOUI",
                    Url = new Uri("https://www.linkedin.com/in/bouchaib-massioui/")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/license/mit/")
                }
            });
        }
    }

    private static void ConfigureSwaggerSecurity(SwaggerGenOptions options)
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme." +
                       "\r\n\r\n Enter 'Bearer'[space] and then your token in the text input below." +
                       "\r\n\r\nExample: \"Bearer 12345abcdef\"",
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
    }

    private static void ConfigureSwaggerXmlComments(SwaggerGenOptions options)
    {
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    }
}
