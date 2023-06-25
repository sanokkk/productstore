using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shops.Domain.Domain.ManyToManyModels;

public class ProductsWithTypes
{
    public int ProductId { get; init; }
    public Product? Product { get; set; }
    
    public int TypeId { get; init; }
    public ProductType? ProductType { get; set; }
}