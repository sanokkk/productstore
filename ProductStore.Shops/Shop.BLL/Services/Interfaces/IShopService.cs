using ProductStore.Shops.Shop.BLL.Dtos.Responses.Shops;

namespace ProductStore.Shops.Shop.BLL.Services.Interfaces;

public interface IShopService
{
    Task<GetAllShopsResponse> GetShopsAsync(CancellationToken cancellationToken);
    Task<GetShopProductsResponse> GetShopProductsAsync(int shopId, CancellationToken cancellationToken);

    Task<GetProductQuantityResponse> GetProductQuantityAsync(int shopId,
        CancellationToken cancellationToken);
}