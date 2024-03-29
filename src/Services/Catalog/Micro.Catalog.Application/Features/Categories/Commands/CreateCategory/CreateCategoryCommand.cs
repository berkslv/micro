using MediatR;
using Micro.Catalog.Application.Common.Interfaces;
using Micro.Catalog.Domain.Entities;
using Micro.Catalog.Domain.Events.Categories;

namespace Micro.Catalog.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<string>
{
    public string Name { get; set; } = null!;
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, string>
{
    private readonly IApplicationDbContext _context;

    public CreateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = new Category
        {
            Name = request.Name,
        };
        
        entity.AddDomainEvent(new CategoryCreatedEvent(entity));
        
        _context.Categories.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}