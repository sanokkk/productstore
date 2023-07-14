using UI.Service.Responses;
using UI.UI.Domain.Models;

namespace UI.Service.Interfaces;

public interface ICardService
{
    Task<GetAllCardsResponse> GetCardsAsync();
}