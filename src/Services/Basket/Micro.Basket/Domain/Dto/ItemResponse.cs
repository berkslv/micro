using Micro.Basket.Domain.Entity;
using Micro.Basket.Domain.Mappings;

namespace Micro.Basket.Domain.Dto;

public class ItemResponse 
{
    public string ProductId { get; set; } = null!;
    
    public string Name { get; set; } = null!;
    
    public int Quantity { get; set; }
    
    public decimal Price { get; set; }
}