using MassTransit;
using Micro.Catalog.Application.Common.Interfaces;
using Micro.Catalog.Domain.Views;
using Micro.Catalog.Domain.Events.Products;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Micro.Catalog.Application.Features.Products.EventHandlers;

public class ProductCreatedEventHandler : IConsumer<ProductCreatedEvent>
{
    private readonly ILogger<ProductCreatedEventHandler> _logger;
    private readonly IReadDbContext _context;

    public ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger, IReadDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
    {
        var product = context.Message.Item;
        
        var view = new ProductView
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price
        };
        
        var options = new InsertOneOptions {BypassDocumentValidation = false};
        await _context.Products.InsertOneAsync(view, options);
    }
}