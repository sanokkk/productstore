using UI.Service.Responses;

namespace UI.Service.Interfaces;

public interface IShopService
{
    Task<GetAllShopsResponse> GetShopsAsync(CancellationToken cancellationToken);
}