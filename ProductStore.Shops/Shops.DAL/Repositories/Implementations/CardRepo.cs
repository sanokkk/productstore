using Microsoft.EntityFrameworkCore;
using ProductStore.Shops.Shops.DAL.Repositories.Interfaces;
using ProductStore.Shops.Shops.Domain.Domain.ManyToManyModels;
using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shops.DAL.Repositories.Implementations;

public class CardRepo: BaseRepo<Card>, ICardRepo
{
    
    public CardRepo(ShopsContext context) : base(context)
    {
    }


    public async Task AddProductToCardAsync(int cardId, Product product, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Card card = (await _context.Cards.FirstOrDefaultAsync(x => x.Id == cardId, cancellationToken))!;

        var newItem = new ProductCard()
        {
            CardId = card.Id,
            Card = card,
            Product = product,
            ProductId = product.Id
        };
        card.ProductsCards.Add(newItem);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<ICollection<Card>> GetAllCardsAsync(string userId, CancellationToken cancellationToken)
    {
        return await _context.Cards
            .Where(x => x.UserId == userId)
            .ToArrayAsync(cancellationToken);
    }
    
}