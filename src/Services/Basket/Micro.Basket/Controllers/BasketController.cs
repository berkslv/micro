using Micro.Basket.Domain.Dto;
using Micro.Basket.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Micro.Basket.Controllers;


public class BasketController : ApiControllerBase
{
    private readonly IBasketService _basketService;

    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }
    
    
    [HttpGet]
    public async Task<ActionResult<CartResponse>> GetBasket()
    {
        return await _basketService.GetBasketAsync();
    }
    
    [HttpPost]
    public async Task<ActionResult<bool>> SetBasket(CartRequest request)
    {
        return await _basketService.SetBasketAsync(request);
    }

    [HttpDelete]
    public ActionResult<bool> DeleteBasket()
    {
        return _basketService.DeleteBasket();
    }
}