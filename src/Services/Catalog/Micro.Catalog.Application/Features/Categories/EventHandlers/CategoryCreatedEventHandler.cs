using MassTransit;
using MediatR;
using Micro.Catalog.Application.Common.Interfaces;
using Micro.Catalog.Domain.Views;
using Micro.Catalog.Domain.Events.Categories;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Micro.Catalog.Application.Features.Categories.EventHandlers;

public class CategoryCreatedEventHandler : IConsumer<CategoryCreatedEvent>
{
    private readonly ILogger<CategoryCreatedEventHandler> _logger;
    private readonly IReadDbContext _context;

    public CategoryCreatedEventHandler(ILogger<CategoryCreatedEventHandler> logger, IReadDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Consume(ConsumeContext<CategoryCreatedEvent> context)
    {
        var category = context.Message.Item;
        
        var view = new CategoryView
        {
            Id = category.Id,
            Name = category.Name,
            LastModified = category.LastModified
        };
        
        var options = new InsertOneOptions {BypassDocumentValidation = false};
        await _context.Categories.InsertOneAsync(view, options);
    }
}