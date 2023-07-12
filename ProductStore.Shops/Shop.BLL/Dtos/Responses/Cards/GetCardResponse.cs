using ProductStore.Shops.Shop.BLL.Dtos.Models;

namespace ProductStore.Shops.Shop.BLL.Dtos.Responses.Cards;

public class GetCardResponse: BaseResponse
{
    public GetCardDto Card { get; set; }
}