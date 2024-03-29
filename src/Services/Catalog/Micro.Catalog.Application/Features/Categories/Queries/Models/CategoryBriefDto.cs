using Micro.Catalog.Application.Common.Mappings;
using Micro.Catalog.Domain.Views;

namespace Micro.Catalog.Application.Features.Categories.Queries.Models;

public class CategoryBriefDto : IMapFrom<CategoryView>
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
}