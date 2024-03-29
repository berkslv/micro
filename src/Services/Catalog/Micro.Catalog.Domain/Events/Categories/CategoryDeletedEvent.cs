using Micro.Catalog.Domain.Common;
using Micro.Catalog.Domain.Entities;

namespace Micro.Catalog.Domain.Events.Categories;

public class CategoryDeletedEvent
{
    public CategoryDeletedEvent(Category item)
    {
        Item = item;
    }
    
    public Category Item { get; set; }
}