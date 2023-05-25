using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ApiGateway.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services
            .AddMediatR(mediatRServiceConfiguration => mediatRServiceConfiguration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}
