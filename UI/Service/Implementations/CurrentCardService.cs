using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Http;
using UI.Service.Interfaces;
using UI.Service.Responses;
using UI.UI.Domain.Models;

namespace UI.Service.Implementations;

public class CurrentCardService: ICurrentCardService
{
    private readonly IHttpClientFactory _factory;
    private readonly HttpClient _client;
    private readonly ILocalStorageService _localStorage;
    private readonly Uri PATH = new Uri("http://localhost:5125/api/Card");
    private readonly ILogger<CurrentCardService> _logger;

    public CurrentCardService(IHttpClientFactory factory, ILocalStorageService localStorage, ILogger<CurrentCardService> logger)
    {
        _factory = factory;
        _localStorage = localStorage;
        _logger = logger;
        _client = factory.CreateClient("Sanokkk");
    }

    public async Task<bool> AddProductToCard(Product product, int shopId)
    {
        try
        {
            var currentCard = await GetCardAsync(shopId);
            if (currentCard.ShopId == shopId)
            {
                if (!currentCard.ProductsWithQuantity.ContainsKey(product.Id))
                    currentCard.ProductsWithQuantity.Add(product.Id, 1);
                else
                    currentCard.ProductsWithQuantity[product.Id] += 1;
                currentCard.TotalPrice += product.Price;
                await SetCard(currentCard);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while adding product to card: {ex.Message}");
            return false;
        }

        return true;
    }

    public async Task<bool> IsCartPay(double price)
    {
        Uri uri = new Uri("http://localhost:5243/api/User");

        var content = JsonContent.Create(price);

        try
        {
            var request = await _client.PostAsync(uri, content);
            if (request.IsSuccessStatusCode)
                return true;
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while paying cart: {ex.Message}");
            return false;
        }
    }

    public async Task<AddCardResponse> AddCardAsync(Card card)
    {
        var response = new AddCardResponse();
        try
        {
            var cardRequest = new
            {
                UserId = "",
                ProductsWithQuantity = card.ProductsWithQuantity,
                TotalPrice = card.TotalPrice,
                ShopId = card.ShopId
            };
            
            var result = await _client.PostAsync("http://localhost:5125/api/Card", JsonContent.Create(cardRequest));
            if (result.IsSuccessStatusCode)
            {
                await _localStorage.RemoveItemAsync("card");
                return response;
            }
            else
            {
                response.Success = false;
                return response;
            }
        }
        catch (Exception ex)
        {
            response.Success = false;
            _logger.LogError($"Error while adding card: {ex.Message}");
        }

        return response;
    }

    public async Task<Card> GetCurrentCardAsync()
    {
        var card = await _localStorage.GetItemAsync<Card>("card");
        return card;
    }

    private async Task<Card> GetCardAsync(int shopId)
    {
        var cardInLS = await _localStorage.GetItemAsync<Card>("card");
        if (cardInLS is not null)
            return cardInLS;
        return new Card(){ShopId = shopId, ProductsWithQuantity = new Dictionary<int, int>()};
    }

    private async Task SetCard(Card card)
    {
        await _localStorage.RemoveItemAsync("card");
        await _localStorage.SetItemAsync("card", card);
    }

    public async Task<GetProductQuantityResponse> GetProductQuantityAsync(int shopId)
    {
        var result = new GetProductQuantityResponse();
        try
        {
            var requestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri($"http://localhost:5125/api/Shop/Quantity/{shopId}"),
                Method = HttpMethod.Get
            };
            var response = await _client.SendAsync(requestMessage);
            if (response.IsSuccessStatusCode)
            {
                result.ProductQuantity = (await response.Content.ReadFromJsonAsync<Dictionary<int, int>>())!;
            }
            else
            {
                result.IsSuccess = false;
                return result;
            }
        }
        catch (NullReferenceException ex)
        {
            result.IsSuccess = false;
            _logger.LogError("Response was null while getting quantity");
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            _logger.LogError($"Error while getting product quantity: {ex.Message} in {nameof(CurrentCardService)}");
        }

        return result;

    }
}