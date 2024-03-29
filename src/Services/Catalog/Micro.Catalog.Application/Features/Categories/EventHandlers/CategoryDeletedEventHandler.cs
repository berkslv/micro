using MassTransit;
using MediatR;
using Micro.Catalog.Application.Common.Interfaces;
using Micro.Catalog.Domain.Views;
using Micro.Catalog.Domain.Events.Categories;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Micro.Catalog.Application.Features.Categories.EventHandlers;

public class CategoryDeletedEventHandler : IConsumer<CategoryDeletedEvent>
{
    private readonly ILogger<CategoryDeletedEventHandler> _logger;
    private readonly IReadDbContext _context;

    public CategoryDeletedEventHandler(ILogger<CategoryDeletedEventHandler> logger, IReadDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Consume(ConsumeContext<CategoryDeletedEvent> context)
    {
        var category = context.Message.Item;

        var view = new CategoryView
        {
            Id = category.Id,
        };
        
        await _context.Categories.FindOneAndDeleteAsync(x => x.Id == view.Id);
    }
}