using Micro.Basket.Domain.Entity;
using Micro.Basket.Domain.Mappings;

namespace Micro.Basket.Domain.Dto;

public class CartRequest
{
    public List<ItemRequest> Items { get; set; } = new List<ItemRequest>();

    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
}