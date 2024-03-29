
using AutoMapper;
using MediatR;
using Micro.Catalog.Application.Common.Interfaces;
using Micro.Catalog.Application.Features.Categories.Queries.Models;
using Micro.Catalog.Domain.Views;
using MongoDB.Driver;

namespace Micro.Catalog.Application.Features.Categories.Queries;

public class GetCategoriesWithPaginationQuery : IRequest<List<CategoryBriefDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetCategoriesWithPaginationQueryHandler : IRequestHandler<GetCategoriesWithPaginationQuery, List<CategoryBriefDto>>
{
    private readonly IReadDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoriesWithPaginationQueryHandler(IReadDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CategoryBriefDto>> Handle(GetCategoriesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<CategoryView>.Filter.Empty;

        var productViews = await _context.Categories
            .Find(filter)
            .SortBy(x => x.LastModified)
            .Skip((request.PageNumber - 1) * request.PageNumber)
            .Limit(request.PageSize)
            .ToListAsync(cancellationToken);

        var products = _mapper.Map<List<CategoryBriefDto>>(productViews);

        return products;
    }
}