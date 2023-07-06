namespace UI.UI.Domain.Models;

public class Product
{
    public int Id { get; init; }
    
    public string Name { get; set; }
    
    public double Price { get; set; }
    
    public string ImagePath { get; set; } = String.Empty;
}