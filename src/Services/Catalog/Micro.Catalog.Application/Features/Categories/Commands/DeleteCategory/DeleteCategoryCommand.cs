using MediatR;
using Micro.Catalog.Application.Common.Exceptions;
using Micro.Catalog.Application.Common.Interfaces;
using Micro.Catalog.Domain.Entities;
using Micro.Catalog.Domain.Events.Categories;

namespace Micro.Catalog.Application.Features.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommand: IRequest
{
    public string Id { get; init; } = null!;
}

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Categories
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity is null) throw new NotFoundException(nameof(Category), request.Id.ToString());
        
        entity.AddDomainEvent(new CategoryDeletedEvent(entity));
        
        _context.Categories.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}