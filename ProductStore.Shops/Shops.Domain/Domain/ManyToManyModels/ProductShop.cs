using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shops.Domain.Domain.ManyToManyModels;

public class ProductShop
{
    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int ShopId { get; set; }
    public Shop Shop { get; set; }
    
    public int Quantity { get; set; }
}