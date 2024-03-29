using Micro.Catalog.Application.Common.Mappings;
using Micro.Catalog.Domain.Views;

namespace Micro.Catalog.Application.Features.Products.Queries.Models;

public class ProductBriefDto : IMapFrom<ProductView>
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public double Price { get; set; }
}