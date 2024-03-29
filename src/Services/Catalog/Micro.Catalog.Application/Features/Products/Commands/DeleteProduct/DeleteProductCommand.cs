using MediatR;
using Micro.Catalog.Application.Common.Exceptions;
using Micro.Catalog.Application.Common.Interfaces;
using Micro.Catalog.Domain.Entities;
using Micro.Catalog.Domain.Events.Products;

namespace Micro.Catalog.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest
{
    public string Id { get; init; } = null!;
}

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity is null) throw new NotFoundException(nameof(Product), request.Id.ToString());
        
        entity.AddDomainEvent(new ProductDeletedEvent(entity));
        
        _context.Products.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}