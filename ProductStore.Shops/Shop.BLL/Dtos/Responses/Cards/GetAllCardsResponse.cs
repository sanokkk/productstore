using ProductStore.Shops.Shop.BLL.Dtos.Models;

namespace ProductStore.Shops.Shop.BLL.Dtos.Responses.Cards;

public class GetAllCardsResponse: BaseResponse
{
    public ICollection<AllCardsDto> Cards { get; set; }
}