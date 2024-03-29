using Micro.Catalog.Domain.Entities;

namespace Micro.Catalog.Domain.Events.Categories;

public class CategoryUpdatedEvent
{
    public CategoryUpdatedEvent(Category item)
    {
        Item = item;
    }
    
    public Category Item { get; set; }
}