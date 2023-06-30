using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shop.BLL.Dtos.Responses.Products;

public class GetAllResponse: BaseResponse
{
    public IEnumerable<Product> Products { get; set; }
}