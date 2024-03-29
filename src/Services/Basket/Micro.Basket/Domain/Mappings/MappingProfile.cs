using System.Reflection;
using AutoMapper;
using Micro.Basket.Domain.Dto;
using Micro.Basket.Domain.Entity;

namespace Micro.Basket.Domain.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Cart, CartRequest>().ReverseMap();
        CreateMap<Cart, CartResponse>().ReverseMap();

        CreateMap<Item, ItemRequest>().ReverseMap();
        CreateMap<Item, ItemResponse>().ReverseMap();
    }

}