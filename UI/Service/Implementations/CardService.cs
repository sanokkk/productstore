using System.Net.Http.Json;
using UI.Service.Interfaces;
using UI.Service.Responses;
using UI.UI.Domain.Dto_S;
using UI.UI.Domain.Models;

namespace UI.Service.Implementations;

public class CardService: ICardService
{
    private readonly IHttpClientFactory _factory;
    private readonly HttpClient _client;
    private readonly Uri PATH = new Uri("http://localhost:5125/api/Card");
    private readonly ILogger<CurrentCardService> _logger;

    public CardService(IHttpClientFactory factory, HttpClient client, ILogger<CurrentCardService> logger)
    {
        _factory = factory;
        _client = _factory.CreateClient("Sanokkk");
        _logger = logger;
    }

    public async Task<GetAllCardsResponse> GetCardsAsync()
    {
        var response = new GetAllCardsResponse();
        try
        {
            response.Cards = (await _client.GetFromJsonAsync<PreviousCart[]>(PATH))!;
        }
        catch (NullReferenceException ex)
        {
            response.Success = false;
            _logger.LogError("Cards were null");
        }
        catch (HttpRequestException ex)
        {
            response.Success = false;
            _logger.LogError($"Fetch error: {ex.Message} with StatusCode: {ex.StatusCode}");
        }
        catch (Exception ex)
        {
            response.Success = false;
            _logger.LogError($"Error while getting cards: {ex.Message}");
        }

        return response;
    }
}