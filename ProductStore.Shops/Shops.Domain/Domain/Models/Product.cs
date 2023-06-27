using ProductStore.Shops.Shops.Domain.Domain.ManyToManyModels;

namespace ProductStore.Shops.Shops.Domain.Domain.Models;

public class Product
{
    public int Id { get; init; }
    
    public string Name { get; set; }
    
    public double Price { get; set; }
    public List<ProductsWithTypes> ProductsWithTypes { get; set; } = new();

    public List<ProductShop> ProductsShops { get; set; } = new();

    public List<ProductCard> ProductsCards { get; set; } = new();
}