using MassTransit;
using Micro.Catalog.Application.Common.Interfaces;
using Micro.Catalog.Infrastructure.Persistence.Application;
using Micro.Catalog.Infrastructure.Persistence.Application.Interceptors;
using Micro.Catalog.Infrastructure.Persistence.View;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Conventions;

namespace Micro.Catalog.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMainDbContext(configuration);

        services.AddReadDbContext();
        
        services.AddSingleton(TimeProvider.System);
        
        return services;
    }

    private static IServiceCollection AddMainDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var postgresConnectionString = configuration.GetConnectionString("MainConnection");
        if (postgresConnectionString is null) throw new ArgumentNullException($"Connection string 'DefaultConnection' not found.");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseNpgsql(postgresConnectionString).UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<ApplicationDbContextInit>();
        
        return services;
    }

    private static IServiceCollection AddReadDbContext(this IServiceCollection services)
    {
        services.AddScoped<IReadDbContext, ReadDbContext>();
        
        var camelCaseConventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("CamelCase", camelCaseConventionPack, type => true);
        
        return services;
    }

}