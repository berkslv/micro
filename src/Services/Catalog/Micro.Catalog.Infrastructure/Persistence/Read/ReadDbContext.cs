using Micro.Catalog.Application.Common.Interfaces;
using Micro.Catalog.Domain.Views;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Micro.Catalog.Infrastructure.Persistence.View;

public class ReadDbContext : IReadDbContext
{
    private readonly IMongoDatabase _database;
    public ReadDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ViewConnection");
        if (connectionString is null) throw new ArgumentNullException($"Connection string 'ViewConnection' not found.");
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase("catalog");
    }
    
    public IMongoCollection<ProductView> Products => _database.GetCollection<ProductView>(nameof(ProductView).ToLowerInvariant());
    public IMongoCollection<CategoryView> Categories => _database.GetCollection<CategoryView>(nameof(CategoryView).ToLowerInvariant());
}