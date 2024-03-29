using MediatR;
using Micro.Catalog.Application.Common.Interfaces;
using Micro.Catalog.Domain.Entities;
using Micro.Catalog.Domain.Events.Products;

namespace Micro.Catalog.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<string>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public double Price { get; set; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, string>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price
        };

        entity.AddDomainEvent(new ProductCreatedEvent(entity));
        
        _context.Products.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}