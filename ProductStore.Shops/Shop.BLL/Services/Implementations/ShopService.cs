using System.Reflection.Metadata;
using AutoMapper;
using ProductStore.Shops.Shop.BLL.Dtos.Models;
using ProductStore.Shops.Shop.BLL.Dtos.Responses.Shops;
using ProductStore.Shops.Shop.BLL.Services.Interfaces;
using ProductStore.Shops.Shops.DAL.Repositories.Interfaces;

namespace ProductStore.Shops.Shop.BLL.Services.Implementations;

public class ShopService: IShopService
{
    private readonly IShopRepo _shopRepo;
    private readonly IProductRepo _productRepo;
    private readonly ILogger<ShopService> _logger;
    private readonly IMapper _mapper;

    public ShopService(IShopRepo shopRepo, ILogger<ShopService> logger, IMapper mapper, IProductRepo productRepo)
    {
        _shopRepo = shopRepo;
        _logger = logger;
        _mapper = mapper;
        _productRepo = productRepo;
    }

    public async Task<GetAllShopsResponse> GetShopsAsync(CancellationToken cancellationToken)
    {
        var response = new GetAllShopsResponse();
        try
        {
            var shops = (await _shopRepo.GetAllAsync(cancellationToken)).ToArray();
            response.Shops = _mapper.Map<GetShopDto[]>(shops);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while getting shops: {ex.Message}");
            response.IsSuccess = false;
        }

        return response;
    }

    public async Task<GetShopProductsResponse> GetShopProductsAsync(int shopId, CancellationToken cancellationToken)
    {
        var result = new GetShopProductsResponse();
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            var products = await _productRepo.GetByShopAsync(shopId, cancellationToken);
            result.Products = _mapper.Map<GetProductDto[]>(products);
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogError("Operation was canceled");
            result.IsSuccess = false;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while getting products in store-{shopId}: {ex.Message}");
            result.IsSuccess = false;
        }

        return result;
    }

    public async Task<GetProductQuantityResponse> GetProductQuantityAsync(int shopId,
        CancellationToken cancellationToken)
    {
        var result = new GetProductQuantityResponse();
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            result.ProductQuantity = await _shopRepo.GetProductQuantityAsync(shopId, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while getting product quantity");
            result.IsSuccess = false;
        }

        return result;
    }
}