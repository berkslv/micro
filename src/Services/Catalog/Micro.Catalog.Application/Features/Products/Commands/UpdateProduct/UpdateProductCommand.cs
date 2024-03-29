using MediatR;
using Micro.Catalog.Application.Common.Exceptions;
using Micro.Catalog.Application.Common.Interfaces;
using Micro.Catalog.Domain.Entities;
using Micro.Catalog.Domain.Events.Products;

namespace Micro.Catalog.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest
{
    public string Id { get; init; } = null!;

    public string? Name { get; init; }

    public string? Description { get; set; }

    public double? Price { get; set; }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        entity.Name = request.Name ?? entity.Name;
        entity.Description = request.Description ?? entity.Description;
        entity.Price = request.Price ?? entity.Price;
        
        entity.AddDomainEvent(new ProductUpdatedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}