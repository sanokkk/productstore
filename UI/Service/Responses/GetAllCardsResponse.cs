using UI.UI.Domain.Dto_S;
using UI.UI.Domain.Models;

namespace UI.Service.Responses;

public class GetAllCardsResponse
{
    public bool Success { get; set; } = true;
    public PreviousCart[] Cards { get; set; }
}