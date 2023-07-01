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
    private readonly ILogger<ShopService> _logger;
    private readonly IMapper _mapper;

    public ShopService(IShopRepo shopRepo, ILogger<ShopService> logger, IMapper mapper)
    {
        _shopRepo = shopRepo;
        _logger = logger;
        _mapper = mapper;
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
}