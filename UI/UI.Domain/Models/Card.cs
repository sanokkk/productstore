namespace UI.UI.Domain.Models;

public class Card
{
    public int Id { get; set; }
    public double TotalPrice { get; set; }
    public Dictionary<int,int> ProductsWithQuantity { get; set; }
    public int ShopId { get; set; }

}