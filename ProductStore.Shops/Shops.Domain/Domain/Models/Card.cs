using ProductStore.Shops.Shops.Domain.Domain.ManyToManyModels;

namespace ProductStore.Shops.Shops.Domain.Domain.Models;

public class Card
{
    public int Id { get; init; }
    public string UserId { get; init; }
    public double TotalPrice { get; set; }

    public List<ProductCard> ProductsCards { get; set; } = new();

}