using MediatR;
using Micro.Catalog.Application.Common.Exceptions;
using Micro.Catalog.Application.Common.Interfaces;
using Micro.Catalog.Domain.Entities;
using Micro.Catalog.Domain.Events.Categories;

namespace Micro.Catalog.Application.Features.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest
{
    public string Id { get; init; } = null!;

    public string? Name { get; init; }

}

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Categories
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Category), request.Id);
        }

        entity.Name = request.Name ?? entity.Name;
        
        entity.AddDomainEvent(new CategoryUpdatedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}