using MediatR;
using Micro.Catalog.Application.Common.Exceptions;
using Micro.Catalog.Application.Common.Interfaces;
using Micro.Catalog.Domain.Entities;
using Micro.Catalog.Domain.Events.Categories;
using Microsoft.EntityFrameworkCore;

namespace Micro.Catalog.Application.Features.Categories.Commands.AddProductToCategory;

public class AddProductToCategoryCommand : IRequest
{
    public string Id { get; init; } = null!;

    public string ProductId { get; init; } = null!;
}

public class AddProductToCategoryCommandHandler : IRequestHandler<AddProductToCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public AddProductToCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddProductToCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .Include(x => x.Products)
            .ThenInclude(x => x.Categories)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (category is null)
        {
            throw new NotFoundException(nameof(Category), request.Id);
        }

        var product = await _context.Products
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

        if (product is null)
        {
            throw new NotFoundException(nameof(Products), request.ProductId);
        }

        if (category.Products.Any(x => x.Id == product.Id))
        {
            throw new BadRequestException("Relationship between Category and Products is already defined.  ");
        }

        category.Products.Add(product);

        category.AddDomainEvent(new CategoryProductAddEvent(category, product));

        await _context.SaveChangesAsync(cancellationToken);
    }
}