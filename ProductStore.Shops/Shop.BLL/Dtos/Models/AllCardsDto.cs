namespace ProductStore.Shops.Shop.BLL.Dtos.Models;

public class AllCardsDto
{
    public int Id { get; init; }
    public string UserId { get; init; }
    public double TotalPrice { get; set; } = 0;
}