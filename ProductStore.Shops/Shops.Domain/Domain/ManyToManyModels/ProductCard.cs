using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shops.Domain.Domain.ManyToManyModels;

public class ProductCard
{
    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int CardId { get; set; }
    public Card Card { get; set; }
    
    public int Quantity { get; set; }
}