using System.Net;
using System.Net.Mime;
using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shops.DAL.Repositories.Interfaces;

public interface IProductRepo: IBaseRepo<Product>
{
    Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken);
    
    Task<ICollection<Product>> GetByShopAsync(int shopId, CancellationToken cancellationToken);

    Task<ICollection<Product>> GetByCardAsync(int cardId, CancellationToken cancellationToken);

    Task<ICollection<Product>> GetByTypeAsync(int typeId, CancellationToken cancellationToken);

    Task<ICollection<Product>> GetAllAsync(CancellationToken cancellationToken);
}