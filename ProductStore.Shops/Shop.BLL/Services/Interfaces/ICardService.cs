using ProductStore.Shops.Shop.BLL.Dtos.Requests.Card;
using ProductStore.Shops.Shop.BLL.Dtos.Responses.Cards;

namespace ProductStore.Shops.Shop.BLL.Services.Interfaces;

public interface ICardService
{
    Task<AddCardResponse> AddCardAsync(AddCardRequest cardRequest, CancellationToken cancellationToken);
    Task<GetAllCardsResponse> GetAllCardsAsync(string userId, CancellationToken cancellationToken);
    Task<GetCardResponse> GetCardByIdAsync(int id, CancellationToken cancellationToken);
}