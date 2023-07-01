using ProductStore.Shops.Shop.BLL.Dtos.Models;

namespace ProductStore.Shops.Shop.BLL.Dtos.Responses.Shops;

public class GetAllShopsResponse: BaseResponse
{
    public GetShopDto[] Shops { get; set; }

}