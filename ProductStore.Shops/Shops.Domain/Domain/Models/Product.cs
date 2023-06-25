using ProductStore.Shops.Shops.Domain.Domain.ManyToManyModels;

namespace ProductStore.Shops.Shops.Domain.Domain.Models;

public class Product
{
    public int Id { get; init; }
    
    public string Name { get; set; }

    public List<ProductType> ProductTypes { get; set; } = new();
    public List<ProductsWithTypes> ProductsWithTypes { get; set; } = new();
}