using ProductStore.Shops.Shops.Domain.Domain.ManyToManyModels;

namespace ProductStore.Shops.Shops.Domain.Domain.Models;

public class ProductType
{
    public int Id { get; init; }
    
    public string Type { get; set; }

    public List<Product> Products { get; set; } = new();
    public List<ProductsWithTypes> ProductsWithTypes { get; set; } = new();
}