using MassTransit;
using Micro.Catalog.Application.Common.Interfaces;
using Micro.Catalog.Domain.Views;
using Micro.Catalog.Domain.Events.Categories;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Micro.Catalog.Application.Features.Categories.EventHandlers;

public class CategoryProductRemoveEventHandler : IConsumer<CategoryProductRemoveEvent>
{
    private readonly ILogger<CategoryProductRemoveEventHandler> _logger;
    private readonly IReadDbContext _context;

    public CategoryProductRemoveEventHandler(ILogger<CategoryProductRemoveEventHandler> logger, IReadDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Consume(ConsumeContext<CategoryProductRemoveEvent> context)
    {
        var category = context.Message.Category;
        var product = context.Message.Product;
        
        var filterForCategory = Builders<CategoryView>.Filter.Eq(p => p.Id, category.Id);
        var updateForCategory = Builders<CategoryView>.Update.Pull(p => p.ProductIds, product.Id);

        await _context.Categories.FindOneAndUpdateAsync(filterForCategory, updateForCategory);
        
        var filterForProduct = Builders<ProductView>.Filter.Eq(p => p.Id, product.Id);
        var updateForProduct = Builders<ProductView>.Update.Pull(p => p.CategoryIds, category.Id);

        await _context.Products.FindOneAndUpdateAsync(filterForProduct, updateForProduct);
    }
}