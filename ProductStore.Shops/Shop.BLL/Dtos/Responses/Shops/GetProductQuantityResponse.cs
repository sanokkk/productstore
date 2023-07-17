namespace ProductStore.Shops.Shop.BLL.Dtos.Responses.Shops;

public class GetProductQuantityResponse: BaseResponse
{
    public Dictionary<int,int> ProductQuantity { get; set; }
}