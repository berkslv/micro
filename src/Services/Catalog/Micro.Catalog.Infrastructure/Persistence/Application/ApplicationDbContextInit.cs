using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Micro.Catalog.Infrastructure.Persistence.Application;

public static class InitExtensions
{
    public static void InitDatabase(this WebApplication app)
    {
        app.InitDatabaseAsync().Wait();
    }
    
    private static async Task InitDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var init = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInit>();

        await init.InitialiseAsync();
    }
}

public class ApplicationDbContextInit
{
    private readonly ILogger<ApplicationDbContextInit> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInit(ILogger<ApplicationDbContextInit> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }
}