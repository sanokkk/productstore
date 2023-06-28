using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shops.DAL.Repositories.Interfaces;

public interface IShopRepo: IBaseRepo<Shop>
{
    Task<Shop> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<ICollection<Shop>> GetAllAsync(CancellationToken cancellationToken);
}