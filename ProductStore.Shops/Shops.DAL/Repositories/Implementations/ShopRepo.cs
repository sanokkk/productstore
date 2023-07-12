using Microsoft.EntityFrameworkCore;
using ProductStore.Shops.Shops.DAL.Repositories.Interfaces;
using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shops.DAL.Repositories.Implementations;

public class ShopRepo: BaseRepo<Domain.Domain.Models.Shop>, IShopRepo
{
    public ShopRepo(ShopsContext _context)
    :base(_context)
    {
    }
    
    public async Task<Domain.Domain.Models.Shop> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await base.GetByIdAsync(f => f.Id == id, cancellationToken);
    }

    public async Task<ICollection<Domain.Domain.Models.Shop>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Shops.ToArrayAsync(cancellationToken);
    }

    public async Task DecreaseProductQuantityAsync(int shopId, int productId, int quantity)
    {
        (await _context.ProductsShops
                .FirstOrDefaultAsync(x => x.ShopId == shopId && x.ProductId == productId))!
            .Quantity -= quantity;
        await _context.SaveChangesAsync();
    }
}