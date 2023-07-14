using UI.Service.Responses;
using UI.UI.Domain.Models;

namespace UI.Service.Interfaces;

public interface ICurrentCardService
{
    Task<bool> AddProductToCard(Product product, int shopId);
    Task<AddCardResponse> AddCardAsync(Card card);
    Task<Card> GetCurrentCardAsync();
}