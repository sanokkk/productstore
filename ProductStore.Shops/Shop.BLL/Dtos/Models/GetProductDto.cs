namespace ProductStore.Shops.Shop.BLL.Dtos.Models;

public class GetProductDto
{
    public int Id { get; init; }
    
    public string Name { get; set; }
    
    public double Price { get; set; }
    
    public string ImagePath { get; set; } = String.Empty;
}