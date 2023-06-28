using Microsoft.EntityFrameworkCore;
using ProductStore.Shops.Shops.DAL.Repositories.Interfaces;
using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shops.DAL.Repositories.Implementations;

public class ProductTypeRepo : BaseRepo<ProductType>, IProductTypeRepo
{
    public ProductTypeRepo(ShopsContext _context)
    :base(_context)
    {
    }
    
    public async Task<ProductType> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await base.GetByIdAsync(f => f.Id == id, cancellationToken);
    }

    public async Task<ICollection<ProductType>> GetByProductAsync(int productId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return (await _context.ProductsWithTypes
            .Where(f => f.ProductId == productId)
            .Select(s => s.ProductType)
            .ToArrayAsync(cancellationToken))!;
    }
}