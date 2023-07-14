using UI.UI.Domain.Models;

namespace UI.Service.Responses;

public class GetAllCardsResponse
{
    public bool Success { get; set; } = true;
    public Card[] Cards { get; set; }
}