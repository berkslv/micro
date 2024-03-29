using Micro.Catalog.Domain.Entities;

namespace Micro.Catalog.Domain.Events.Categories;

public class CategoryProductRemoveEvent
{
    public CategoryProductRemoveEvent(Category category, Product product)
    {
        Category = category;
        Product = product;
    }
    
    public Category Category { get; set; }
    public Product Product { get; set; }
}