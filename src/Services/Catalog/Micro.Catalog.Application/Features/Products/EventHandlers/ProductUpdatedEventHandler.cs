using MassTransit;
using Micro.Catalog.Application.Common.Interfaces;
using Micro.Catalog.Domain.Views;
using Micro.Catalog.Domain.Events.Products;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Micro.Catalog.Application.Features.Products.EventHandlers;

public class ProductUpdatedEventHandler : IConsumer<ProductUpdatedEvent>
{
    private readonly ILogger<ProductUpdatedEventHandler> _logger;
    private readonly IReadDbContext _context;

    public ProductUpdatedEventHandler(ILogger<ProductUpdatedEventHandler> logger, IReadDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Consume(ConsumeContext<ProductUpdatedEvent> context)
    {
        var product = context.Message.Item;

        var view = new ProductView
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price
        };
        
        await _context.Products.FindOneAndReplaceAsync(x => x.Id == view.Id, view);
    }
}