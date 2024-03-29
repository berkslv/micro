using Micro.Basket.Domain.Dto;

namespace Micro.Basket.Services.Interfaces;

public interface IBasketService
{
    public Task<bool> SetBasketAsync(CartRequest request);
    public Task<CartResponse> GetBasketAsync();
    public bool DeleteBasket();
}