using System.Net.Http.Json;
using ProductStore.Exceptions.Exceptions.HTTP;
using UI.Service.Interfaces;
using UI.Service.Responses;
using UI.UI.Domain.Models;

namespace UI.Service.Implementations;

public class ShopService: IShopService
{
    private readonly IHttpClientFactory _factory;
    private readonly ILogger<ShopService> _logger;
    private readonly HttpClient _httpClient;
    private Uri ControllerPath;
    

    public ShopService(IHttpClientFactory factory, ILogger<ShopService> logger)
    {
        _factory = factory;
        _logger = logger;
        _httpClient = _factory.CreateClient("Sanokkk");
        ControllerPath = new Uri("http://localhost:5125/api/Shop");
    }

    public async Task<GetAllShopsResponse> GetShopsAsync(CancellationToken cancellationToken)
    {
        
        var result = new GetAllShopsResponse();
        try
        {
            var uri = new Uri("http://localhost:5125/api/Shop");
            var response = await _httpClient.GetAsync(uri);
            //var response = await _httpClient.GetFromJsonAsync<Shop[]>(uri);
            //result.Shops = response;
            if (response.IsSuccessStatusCode)
            {
                var shops = await response.Content.ReadFromJsonAsync<Shop[]>();
                result.Shops = shops;
            }
            else
            {
                throw new NotSuccessResponseException();
            }
        }
        catch (NotSuccessResponseException ex)
        {
            _logger.LogError("Not successful request");
            result.Success = false;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while getting shops: {ex.Message}");
            result.Success = false;
        }

        return result;
    }
}