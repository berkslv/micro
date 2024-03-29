using Micro.Catalog.Domain.Common;
using Micro.Catalog.Domain.Entities;

namespace Micro.Catalog.Domain.Events.Products;

public class ProductDeletedEvent
{
    public ProductDeletedEvent(Product item)
    {
        Item = item;
    }
    
    public Product Item { get; set; }   
}