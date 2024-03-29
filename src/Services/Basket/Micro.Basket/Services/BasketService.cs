using System.Text;
using System.Text.Json;
using AutoMapper;
using Micro.Basket.Domain.Dto;
using Micro.Basket.Domain.Entity;
using Micro.Basket.Domain.Exceptions;
using Micro.Basket.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Micro.Basket.Services;

public class BasketService : IBasketService
{
    private readonly IDistributedCache _distributedCache;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public BasketService(IDistributedCache distributedCache, ICurrentUserService currentUserService, IMapper mapper)
    {
        _distributedCache = distributedCache;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }
    
    public async Task<bool> SetBasketAsync(CartRequest request)
    {
        var cart = _mapper.Map<Cart>(request);
        cart.UserId = _currentUserService.UserId;
        
        await _distributedCache.SetAsync(_currentUserService.UserId, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(cart)));

        return true;
    }
    
    public async Task<CartResponse> GetBasketAsync()
    {
        var basket = await _distributedCache.GetAsync(_currentUserService.UserId);
        var result = basket == null ? null : JsonSerializer.Deserialize<Cart>(Encoding.UTF8.GetString(basket));
        
        if (result is null) throw new NotFoundException(nameof(Cart), _currentUserService.UserId);
        
        return _mapper.Map<CartResponse>(result);
    }

    public bool DeleteBasket()
    {
        var result = _distributedCache.RemoveAsync(_currentUserService.UserId);
        
        return result.IsCompletedSuccessfully;
    }
}