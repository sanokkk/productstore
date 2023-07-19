namespace ProductStore.Shops.Shop.BLL.Dtos.Models;

public class GetCardDto
{
    public Dictionary<int, int> ProductidQuantity { get; set; }
    
    public double TotalPrice { get; set; }
    
    public int ShopId { get; set; }
}