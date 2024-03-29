using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Micro.Catalog.Application.Common.Interfaces;
using Micro.Catalog.Application.Common.Mappings;
using Micro.Catalog.Application.Common.Models;
using Micro.Catalog.Application.Features.Products.Queries.Models;
using Micro.Catalog.Domain.Entities;
using Micro.Catalog.Domain.Views;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Micro.Catalog.Application.Features.Products.Queries;

public class GetProductsWithPaginationQuery : IRequest<List<ProductBriefDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetProductsWithPaginationQueryHandler : IRequestHandler<GetProductsWithPaginationQuery, List<ProductBriefDto>>
{
    private readonly IReadDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsWithPaginationQueryHandler(IReadDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ProductBriefDto>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<ProductView>.Filter.Empty;

        var productViews = await _context.Products
            .Find(filter)
            .SortBy(x => x.LastModified)
            .Skip((request.PageNumber - 1) * request.PageNumber)
            .Limit(request.PageSize)
            .ToListAsync(cancellationToken);

        var products = _mapper.Map<List<ProductBriefDto>>(productViews);

        return products;
    }
}