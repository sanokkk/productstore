using ProductStore.Shops.Shop.BLL.Dtos.Requests.Products;
using ProductStore.Shops.Shop.BLL.Dtos.Responses.Products;
using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shop.BLL.Services.Interfaces;

public interface IProductService
{
    Task<UpdatePhotoResponse> UpdatePhotoAsync(UpdatePhotoRequest request, CancellationToken cancellationToken);

    Task<GetAllResponse> GetAllAsync(CancellationToken cancellationToken);

    Task<GetByIdResponse> GetByIdAsync(int productId, CancellationToken cancellationToken);

    Task<CreateProductResponse> CreeateAsync(CreateProductRequest request,
        CancellationToken cancellationToken);
}