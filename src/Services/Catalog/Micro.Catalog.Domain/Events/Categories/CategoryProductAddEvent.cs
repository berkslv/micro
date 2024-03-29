using Micro.Catalog.Domain.Common;
using Micro.Catalog.Domain.Entities;

namespace Micro.Catalog.Domain.Events.Categories;

public class CategoryProductAddEvent
{
    public CategoryProductAddEvent(Category category, Product product)
    {
        Category = category;
        Product = product;
    }
    
    public Category Category { get; set; }
    public Product Product { get; set; }
}