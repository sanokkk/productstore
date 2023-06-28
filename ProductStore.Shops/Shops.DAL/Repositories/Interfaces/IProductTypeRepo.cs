using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shops.DAL.Repositories.Interfaces;

public interface IProductTypeRepo: IBaseRepo<ProductType>
{
    Task<ProductType> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<ICollection<ProductType>> GetByProductAsync(int productId, CancellationToken cancellationToken);
}