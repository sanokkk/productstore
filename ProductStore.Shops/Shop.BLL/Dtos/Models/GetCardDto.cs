namespace ProductStore.Shops.Shop.BLL.Dtos.Models;

public class GetCardDto
{
    public Dictionary<string, int> ProductPrice { get; set; }
    
    public double TotalPrice { get; set; }
    
    public int ShopId { get; set; }
}