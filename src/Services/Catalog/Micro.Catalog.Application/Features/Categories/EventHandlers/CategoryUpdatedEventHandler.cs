using MassTransit;
using Micro.Catalog.Application.Common.Interfaces;
using Micro.Catalog.Domain.Views;
using Micro.Catalog.Domain.Events.Categories;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Micro.Catalog.Application.Features.Categories.EventHandlers;

public class CategoryUpdatedEventHandler : IConsumer<CategoryUpdatedEvent>
{
    private readonly ILogger<CategoryUpdatedEventHandler> _logger;
    private readonly IReadDbContext _context;

    public CategoryUpdatedEventHandler(ILogger<CategoryUpdatedEventHandler> logger, IReadDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Consume(ConsumeContext<CategoryUpdatedEvent> context)
    {
        var category = context.Message.Item;

        var view = new CategoryView
        {
            Id = category.Id,
            Name = category.Name,
        };
        
        await _context.Categories.FindOneAndReplaceAsync(x => x.Id == view.Id, view);
    }
}