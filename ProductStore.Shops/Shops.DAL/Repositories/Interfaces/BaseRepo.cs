using System.Linq.Expressions;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.EntityFrameworkCore;

namespace ProductStore.Shops.Shops.DAL.Repositories.Interfaces;

public abstract class BaseRepo<T>: IBaseRepo<T> where T : class
{
    protected readonly ShopsContext _context;

    protected BaseRepo(ShopsContext context)
    {
        _context = context;
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task EditAsync(T entity, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<T> GetByIdAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return (await _context.Set<T>()
            .FirstOrDefaultAsync(expression, cancellationToken))!;
    }
}