
using Micro.Basket.Domain.Entity;
using Micro.Basket.Domain.Mappings;

namespace Micro.Basket.Domain.Dto;

public class CartResponse 
{
    public string UserId { get; set; } = null!;
    public List<ItemResponse> Items { get; set; } = new List<ItemResponse>();

    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
    
}