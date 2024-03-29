using Micro.Catalog.Domain.Views;
using MongoDB.Driver;

namespace Micro.Catalog.Application.Common.Interfaces;

public interface IReadDbContext
{
    IMongoCollection<ProductView> Products { get; }
    IMongoCollection<CategoryView> Categories { get; }
}