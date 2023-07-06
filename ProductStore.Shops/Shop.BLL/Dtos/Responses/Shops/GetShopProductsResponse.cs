using ProductStore.Shops.Shop.BLL.Dtos.Models;
using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shop.BLL.Dtos.Responses.Shops;

public class GetShopProductsResponse: BaseResponse
{
    public ICollection<GetProductDto> Products { get; set; }
}