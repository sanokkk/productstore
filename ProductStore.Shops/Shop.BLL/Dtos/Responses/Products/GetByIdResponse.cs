using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shop.BLL.Dtos.Responses.Products;

public class GetByIdResponse: BaseResponse
{
    public Product Product { get; set; }
}