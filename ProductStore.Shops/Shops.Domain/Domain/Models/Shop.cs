using ProductStore.Shops.Shops.Domain.Domain.ManyToManyModels;

namespace ProductStore.Shops.Shops.Domain.Domain.Models;

public class Shop
{
    public int Id { get; set; }
    
    public string Address { get; init; }
    
    public string Name { get; init; }
    
    public List<ProductShop> ProductsShops { get; set; }
}