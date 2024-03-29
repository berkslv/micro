using MassTransit;
using Micro.Catalog.Application.Common.Interfaces;
using Micro.Catalog.Domain.Views;
using Micro.Catalog.Domain.Events.Categories;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;


namespace Micro.Catalog.Application.Features.Categories.EventHandlers;

public class CategoryProductAddEventHandler : IConsumer<CategoryProductAddEvent>
{
    private readonly ILogger<CategoryProductAddEventHandler> _logger;
    private readonly IReadDbContext _context;

    public CategoryProductAddEventHandler(ILogger<CategoryProductAddEventHandler> logger, IReadDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Consume(ConsumeContext<CategoryProductAddEvent> context)
    {
        var category = context.Message.Category;
        var product = context.Message.Product;

        var filterForCategory = Builders<CategoryView>.Filter.Eq(p => p.Id, category.Id);
        var updateForCategory = Builders<CategoryView>.Update.Push(p => p.ProductIds, product.Id);

        await _context.Categories.FindOneAndUpdateAsync(filterForCategory, updateForCategory);
        
        var filterForProduct = Builders<ProductView>.Filter.Eq(p => p.Id, product.Id);
        var updateForProduct = Builders<ProductView>.Update.Push(p => p.CategoryIds, category.Id);

        await _context.Products.FindOneAndUpdateAsync(filterForProduct, updateForProduct);
    }
}