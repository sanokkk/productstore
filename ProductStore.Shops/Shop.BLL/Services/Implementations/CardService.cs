using System.Reflection.Metadata;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductStore.Shops.Shop.BLL.Dtos.Models;
using ProductStore.Shops.Shop.BLL.Dtos.Requests.Card;
using ProductStore.Shops.Shop.BLL.Dtos.Responses.Cards;
using ProductStore.Shops.Shop.BLL.Services.Interfaces;
using ProductStore.Shops.Shops.DAL.Repositories.Interfaces;
using ProductStore.Shops.Shops.Domain.Domain.ManyToManyModels;
using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shop.BLL.Services.Implementations;

public class CardService: ICardService
{
    private readonly ILogger<CardService> _logger;
    private readonly ICardRepo _cardRepo;
    private readonly IShopRepo _shopRepo;
    private readonly IMapper _mapper;

    public CardService(ILogger<CardService> logger, ICardRepo cardRepo, IShopRepo shopRepo, IMapper mapper)
    {
        _logger = logger;
        _cardRepo = cardRepo;
        _shopRepo = shopRepo;
        _mapper = mapper;
    }

    /// <summary>
    /// Метод для добавления корзины товаров пользователя (НУЖНО БУДЕТ ДОБАВИТЬ
    /// КОМАНДУ ДЛЯ MASSTRANSIT - ОБНОВЛЕНИЯ БАЛАНСА ЮЗЕРА)
    /// </summary>
    /// <param name="cardRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<AddCardResponse> AddCardAsync(AddCardRequest cardRequest, CancellationToken cancellationToken)
    {
        var result = new AddCardResponse();
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            Card card = new Card()
            {
                UserId = cardRequest.UserId,
                TotalPrice = cardRequest.TotalPrice,
                ShopId = cardRequest.ShopId
            };
            card.ProductsCards.AddRange(getProductCards(cardRequest, card));
            //card.ProductsCards.AddRange(getProductCards(card, cardRequest.ProductsWithQuantity.Keys));
            await _cardRepo.AddAsync(card, cancellationToken);
            await DecreaseQuantityAsync(cardRequest.ShopId, cardRequest.ProductsWithQuantity);
        }
        catch (OperationCanceledException ex)
        {
            result.IsSuccess = false;
            _logger.LogError("Operation of adding card was canceled");
        }
        catch (DbUpdateException ex)
        {
            result.IsSuccess = false;
            _logger.LogError($"Error while updating data in Db: {ex.Message}");
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            _logger.LogError($"Error while adding card: {ex.Message}");
        }

        return result;
    }

    public async Task<GetAllCardsResponse> GetAllCardsAsync(string userId, CancellationToken cancellationToken)
    {
        var response = new GetAllCardsResponse();
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            var cards = await _cardRepo.GetAllCardsAsync(userId, cancellationToken);
            response.Cards = _mapper.Map<AllCardsDto[]>(cards);
        }
        catch (OperationCanceledException ex)
        {
            response.IsSuccess = false;
            _logger.LogError("Operation was canceled");
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            _logger.LogError($"Error while getting cards: {ex.Message}");
        }

        return response;
    }

    public async Task<GetCardResponse> GetCardByIdAsync(int id, CancellationToken cancellationToken)
    {
        var response = new GetCardResponse();
        try
        {
            var card = await _cardRepo.GetCardByIdAsync(id, cancellationToken); ;
            response.Card = card;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            _logger.LogError($"Error while getting card by id: {ex.Message}");
        }
        return response;
    }

    private Dictionary<string, int> GetProductWithPrice(Card card)
    {
        var dict = new Dictionary<string, int>();
        var products = card.ProductsCards
            .Where(x => x.CardId == card.Id)
            .Select(x => new { Product = x.Product, Quantity = x.Quantity });
        foreach (var prod in products)
        {
            dict.Add(prod.Product.Name, prod.Quantity);
        }
        return dict;
    }

    /*private ICollection<ProductCard> getProductCards(Card card, ICollection<int> productIds)
    {
        return productIds.Select(pc => new ProductCard() {Card = card, ProductId = pc})
            .ToArray();
    }*/
    private ICollection<ProductCard> getProductCards(AddCardRequest request, Card card)
    {
        var res = new List<ProductCard>();
        foreach (var key in request.ProductsWithQuantity.Keys)
        {
            res.Add(new ProductCard()
            {
                Card = card,
                ProductId = key,
                Quantity = request.ProductsWithQuantity[key]
            });
        }

        return res;
    }
    

    private async Task DecreaseQuantityAsync(int shopId, Dictionary<int, int> productIds)
    {
        try
        {
            foreach (var id in productIds.Keys)
            {
                await _shopRepo.DecreaseProductQuantityAsync(shopId, id, productIds[id]);
            }
        }
        catch (NullReferenceException ex)
        {
            _logger.LogError($"Error while updating product's quantity in shop: shop was null");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while updating product's quantity in shop: {ex.Message}");
        }
    }
}