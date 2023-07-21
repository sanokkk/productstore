using Microsoft.EntityFrameworkCore;
using ProductStore.Shops.Shops.DAL.Repositories.Interfaces.Consumer;

namespace ProductStore.Shops.Shops.DAL.Repositories.Implementations.Consumer;

public class ProductStockRepo: IProductStockRepo
{
    private readonly ShopsContext _db;

    public ProductStockRepo(ShopsContext db)
    {
        _db = db;
    }

    public async Task IncreaseStock(int quantity)
    {
        var prodShop = await _db.ProductsShops.ToArrayAsync();
        foreach (var prod in prodShop)
        {
            prod.Quantity += quantity;
        }

        await _db.SaveChangesAsync();
    }
}