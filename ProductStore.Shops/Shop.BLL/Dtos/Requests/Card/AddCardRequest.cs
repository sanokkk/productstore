namespace ProductStore.Shops.Shop.BLL.Dtos.Requests.Card;

public class AddCardRequest
{
    public string UserId { get; set; }
    
    //productId : quantity
    public Dictionary<int, int> ProductsWithQuantity { get; init; }
    public double TotalPrice { get; set; }
    public int ShopId { get; set; }
}