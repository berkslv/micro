namespace Micro.Basket.Domain.Entity;

public class Item
{
    public string ProductId { get; set; } = null!;
    
    public string Name { get; set; } = null!;
    
    public int Quantity { get; set; }
    
    public decimal Price { get; set; }
}