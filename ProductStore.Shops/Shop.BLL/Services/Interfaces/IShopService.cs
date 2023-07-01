using ProductStore.Shops.Shop.BLL.Dtos.Responses.Shops;

namespace ProductStore.Shops.Shop.BLL.Services.Interfaces;

public interface IShopService
{
    Task<GetAllShopsResponse> GetShopsAsync(CancellationToken cancellationToken);
}