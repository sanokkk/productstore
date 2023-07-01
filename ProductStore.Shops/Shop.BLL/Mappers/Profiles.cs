using AutoMapper;
using ProductStore.Shops.Shop.BLL.Dtos.Models;

namespace ProductStore.Shops.Shop.BLL.Mappers;

public class Profiles: Profile
{
    public Profiles()
    {
        CreateMap<Shops.Domain.Domain.Models.Shop, GetShopDto>();
    }
}