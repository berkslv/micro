using System.Reflection;
using FluentValidation;
using MassTransit;
using MediatR;
using Micro.Catalog.Application.Common.Behaviours;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.Catalog.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        });

        var broker = configuration.GetSection("Broker");
        services.AddMassTransit(cfg =>
        {
            cfg.AddConsumers(Assembly.GetExecutingAssembly());
            cfg.SetKebabCaseEndpointNameFormatter();
            cfg.UsingRabbitMq((context, config) =>
            {
                config.Host(broker["Host"], broker["VirtualHost"], h => {
                    h.Username(broker["Username"]);
                    h.Password(broker["Password"]);
                });

                config.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}