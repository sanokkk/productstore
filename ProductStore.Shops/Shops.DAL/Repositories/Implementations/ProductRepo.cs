using Microsoft.EntityFrameworkCore;
using ProductStore.Shops.Shops.DAL.Repositories.Interfaces;
using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shops.DAL.Repositories.Implementations;

public class ProductRepo: IProductRepo
{
    private readonly ShopsContext _context;

    public ProductRepo(ShopsContext context)
    {
        _context = context;
    }


    public async Task AddAsync(Product entity, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _context.Products.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task EditAsync(Product entity, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _context.Products.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Product entity, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _context.Products.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    

    public async Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return (await _context.Products.FirstOrDefaultAsync(f => f.Id == id, cancellationToken))!;
    }

    public async Task<ICollection<Product>> GetByShopAsync(int shopId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return
            await _context.ProductsShops
                .Include(i => i.Product)
                .Include(i => i.Shop)
                .Where(w => w.ShopId == shopId)
                .Select(s => s.Product)
                .ToArrayAsync(cancellationToken);
    }

    public async Task<ICollection<Product>> GetByCardAsync(int cardId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return
            (await _context.ProductsCards
                .Include(i => i.Card)
                .Include(i => i.Product)
                .Where(w => w.CardId == cardId)
                .Select(s => s.Product)
                .ToArrayAsync(cancellationToken))!;
    }

    public async Task<ICollection<Product>> GetByTypeAsync(int typeId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var products = (await _context.ProductsWithTypes.ToArrayAsync(cancellationToken));
        var filter = products.Where(w => w.TypeId == typeId);
        return (await _context.ProductsWithTypes
            .Include(i => i.Product)
            .Where(w => w.TypeId == typeId)
            .Include(i => i.Product)
            .Select(s => s.Product)
            .ToArrayAsync(cancellationToken))!;
    }

    public async Task<ICollection<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Products.ToArrayAsync(cancellationToken);
    }
}