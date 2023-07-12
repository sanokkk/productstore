using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shops.DAL.Repositories.Interfaces;

public interface IShopRepo: IBaseRepo<Domain.Domain.Models.Shop>
{
    Task<Domain.Domain.Models.Shop> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<ICollection<Domain.Domain.Models.Shop>> GetAllAsync(CancellationToken cancellationToken);
    Task DecreaseProductQuantityAsync(int shopId, int productId, int quantity);
}