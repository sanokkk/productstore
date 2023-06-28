using Microsoft.EntityFrameworkCore;
using ProductStore.Shops.Shops.DAL.Repositories.Interfaces;
using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shops.DAL.Repositories.Implementations;

public class ShopRepo: BaseRepo<Shop>, IShopRepo
{
    public ShopRepo(ShopsContext _context)
    :base(_context)
    {
    }
    
    public async Task<Shop> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await base.GetByIdAsync(f => f.Id == id, cancellationToken);
    }

    public async Task<ICollection<Shop>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Shops.ToArrayAsync(cancellationToken);
    }
}