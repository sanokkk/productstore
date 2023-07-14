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
            var content = JsonSerializer.Serialize(card);
            //var result = await _client.PostAsJsonAsync(PATH, content);
            
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
}