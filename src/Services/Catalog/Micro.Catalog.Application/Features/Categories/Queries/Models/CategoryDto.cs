using Micro.Catalog.Application.Common.Mappings;
using Micro.Catalog.Application.Features.Products.Queries.Models;
using Micro.Catalog.Domain.Views;

namespace Micro.Catalog.Application.Features.Categories.Queries.Models;

public class CategoryDto : IMapFrom<CategoryView>
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public ICollection<string> Products { get; set; } = new List<string>();
}