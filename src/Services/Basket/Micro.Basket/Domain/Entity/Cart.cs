
namespace Micro.Basket.Domain.Entity;

public class Cart
{
    public string UserId { get; set; } = null!;
    public List<Item> Items { get; set; } = new List<Item>();

    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
}