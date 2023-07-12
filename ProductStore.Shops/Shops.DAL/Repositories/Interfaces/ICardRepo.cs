using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shops.DAL.Repositories.Interfaces;

public interface ICardRepo: IBaseRepo<Card>
{
    Task AddProductToCardAsync(int cardId, Product product, CancellationToken cancellationToken);
    Task<ICollection<Card>> GetAllCardsAsync(string userId, CancellationToken cancellationToken);
}