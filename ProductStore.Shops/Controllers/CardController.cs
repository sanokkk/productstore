using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductStore.Shops.Shop.BLL.Dtos.Requests.Card;
using ProductStore.Shops.Shop.BLL.Services.Interfaces;

namespace ProductStore.Shops.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CardController: ControllerBase
{
    private readonly ICardService _cardService;

    public CardController(ICardService cardService)
    {
        _cardService = cardService;
    }

    [HttpPost]
    public async Task<IActionResult> AddCardAsync([FromBody] AddCardRequest request,
        CancellationToken cancellationToken)
    {
        request.UserId = User.Claims.First(x => x.Type == "id").Value;
        var response = await _cardService.AddCardAsync(request, cancellationToken);
        if (response.IsSuccess)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> GetCardsAsync(CancellationToken cancellationToken)
    {
        var userId = User.Claims.First(x => x.Type == "id").Value;
        var response = await _cardService.GetAllCardsAsync(userId, cancellationToken);
        if (response.IsSuccess)
        {
            return Ok(response.Cards);
        }

        return BadRequest();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCardByIdAsync([FromRoute] int cardId, CancellationToken cancellationToken)
    {
        var response = await _cardService.GetCardByIdAsync(cardId, cancellationToken);
        if (response.IsSuccess)
        {
            return Ok(response.Card);
        }

        return BadRequest();
    }
}