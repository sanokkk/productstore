using System.Linq.Expressions;
using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shops.DAL.Repositories.Interfaces;

public interface IBaseRepo<T>
{
    Task AddAsync(T entity, CancellationToken cancellationToken);
    Task EditAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);

    
    Task<T> GetByIdAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
}