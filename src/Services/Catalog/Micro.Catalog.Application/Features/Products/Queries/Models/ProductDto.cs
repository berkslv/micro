using Micro.Catalog.Application.Common.Mappings;
using Micro.Catalog.Application.Features.Categories.Queries.Models;
using Micro.Catalog.Domain.Views;

namespace Micro.Catalog.Application.Features.Products.Queries.Models;

public class ProductDto : IMapFrom<ProductView>
{
    public string Id { get; set; } = null!;
    
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public double Price { get; set; }
    
    public ICollection<CategoryBriefDto> Categories { get; set; } = new List<CategoryBriefDto>();   
}