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
            if (response.IsSuccessStatusCode)
            {
                var shops = await response.Content.ReadFromJsonAsync<Shop[]>();
                result.Shops = shops!;
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

    public async Task<GetShopProductsResponse> GetShopProductsAsync(string shopId, CancellationToken token)
    {
        var result = new GetShopProductsResponse();
        try
        {
            token.ThrowIfCancellationRequested();
            result.Products = (await _httpClient.GetFromJsonAsync<Product[]>(ControllerPath + "/" + shopId, token))!;
        }
        catch (NullReferenceException ex)
        {
            _logger.LogError("Null products list");
            result.Success = false;
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogError("Operation was canceled");
            result.Success = false;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while getting products in shop: {ex.Message}");
            result.Success = false;
        }

        return result;
    }
}