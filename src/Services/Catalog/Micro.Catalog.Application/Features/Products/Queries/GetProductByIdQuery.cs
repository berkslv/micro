using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Micro.Catalog.Application.Common.Exceptions;
using Micro.Catalog.Application.Common.Interfaces;
using Micro.Catalog.Application.Features.Categories.Queries.Models;
using Micro.Catalog.Application.Features.Products.Queries.Models;
using Micro.Catalog.Domain.Views;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Micro.Catalog.Application.Features.Products.Queries;

public class GetProductByIdQuery : IRequest<ProductDto>
{
    public string Id { get; set; } = null!;
}

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IReadDbContext _context;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IReadDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var productView = await _context.Products
            .Find(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (productView is null) throw new NotFoundException(nameof(ProductView), request.Id);

        var product = _mapper.Map<ProductDto>(productView);
        
        var categoriesView = await _context.Categories
            .Find(x => productView.CategoryIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var categories = _mapper.Map<List<CategoryBriefDto>>(categoriesView);

        product.Categories = categories;

        return product;
    }
}