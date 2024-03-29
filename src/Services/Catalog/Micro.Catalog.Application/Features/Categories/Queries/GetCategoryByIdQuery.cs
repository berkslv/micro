using AutoMapper;
using MediatR;
using Micro.Catalog.Application.Common.Exceptions;
using Micro.Catalog.Application.Common.Interfaces;
using Micro.Catalog.Application.Features.Categories.Queries.Models;
using Micro.Catalog.Domain.Views;
using MongoDB.Driver;

namespace Micro.Catalog.Application.Features.Categories.Queries;

public class GetCategoryByIdQuery: IRequest<CategoryDto>
{
    public string Id { get; set; } = null!;
}

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    private readonly IReadDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoryByIdQueryHandler(IReadDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var productView = await _context.Categories
            .Find(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (productView is null) throw new NotFoundException(nameof(CategoryView), request.Id);

        var product = _mapper.Map<CategoryDto>(productView);

        return product;
    }
}