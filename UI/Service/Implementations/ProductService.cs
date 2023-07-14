using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Rendering;
using UI.Service.Interfaces;
using UI.UI.Domain.Models;

namespace UI.Service.Implementations;

public class ProductService: IProductService
{
    private readonly IHttpClientFactory _factory;
    private readonly HttpClient _client;
    private readonly Uri PATH = new Uri("http://localhost:5125/api/Product");
    private readonly ILogger<CurrentCardService> _logger;

    public ProductService(IHttpClientFactory factory, HttpClient client, ILogger<CurrentCardService> logger)
    {
        _factory = factory;
        _client = _factory.CreateClient("Sanokkk");
        _logger = logger;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        try
        {
            return (await _client.GetFromJsonAsync<Product>($"{PATH}/{id}"))!;
        }
        catch (NullReferenceException ex)
        {
            _logger.LogError("Product is null");
            return default(Product);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while getting product by id: {ex.Message}");
            return default(Product);
        }
    }
}