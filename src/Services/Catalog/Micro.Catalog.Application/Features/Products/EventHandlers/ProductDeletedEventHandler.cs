using MassTransit;
using Micro.Catalog.Application.Common.Interfaces;
using Micro.Catalog.Domain.Views;
using Micro.Catalog.Domain.Events.Products;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Micro.Catalog.Application.Features.Products.EventHandlers;

public class ProductDeletedEventHandler : IConsumer<ProductDeletedEvent>
{
    private readonly ILogger<ProductDeletedEventHandler> _logger;
    private readonly IReadDbContext _context;

    public ProductDeletedEventHandler(ILogger<ProductDeletedEventHandler> logger, IReadDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Consume(ConsumeContext<ProductDeletedEvent> context)
    {
        var product = context.Message.Item;

        var view = new ProductView
        {
            Id = product.Id,
        };
        
        await _context.Products.FindOneAndDeleteAsync(x => x.Id == view.Id);
    }
}