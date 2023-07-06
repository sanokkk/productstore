using UI.UI.Domain.Models;

namespace UI.Service.Responses;

public class GetShopProductsResponse: IBaseReponse
{
    public bool Success { get; set; } = true;
    
    public ICollection<Product> Products { get; set; }
}