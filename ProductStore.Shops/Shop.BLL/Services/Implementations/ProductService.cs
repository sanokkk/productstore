using System.Reflection.Metadata;
using Microsoft.VisualBasic;
using ProductStore.Shops.Shop.BLL.Dtos.Requests.Products;
using ProductStore.Shops.Shop.BLL.Dtos.Responses.Products;
using ProductStore.Shops.Shop.BLL.Services.Interfaces;
using ProductStore.Shops.Shops.DAL.Repositories.Interfaces;
using ProductStore.Shops.Shops.DAL.Repositories.Interfaces.ManyToManyInterfaces;
using ProductStore.Shops.Shops.Domain.Domain.ManyToManyModels;
using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shop.BLL.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepo _productRepo;
    private readonly IProductsShopsRepo _productsShops;
    private readonly IProductsWithTypesRepo _productsWithTypes;
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepo productRepo, IWebHostEnvironment environment, ILogger<ProductService> logger, 
        IProductsShopsRepo productsShops, IProductsWithTypesRepo productsWithTypes)
    {
        _productRepo = productRepo;
        _environment = environment;
        _logger = logger;
        _productsShops = productsShops;
        _productsWithTypes = productsWithTypes;
    }

    public async Task<UpdatePhotoResponse> UpdatePhotoAsync(UpdatePhotoRequest request,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var response = new UpdatePhotoResponse();
        try
        {
            var product = await _productRepo.GetByIdAsync(request.productId, cancellationToken);

            var file = request.File;
            var type = file.ContentType.Split("/")[1];
            _logger.LogInformation(file.ContentType);
            var path = Path.Combine(_environment.ContentRootPath, @"wwwroot\Pictures", $"{request.FileName}.{type}");
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }

            product.ImagePath = path;

            await _productRepo.EditAsync(product, cancellationToken);
            response.Product = product;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            _logger.LogError($"Error while uploading file: {ex.Message}");
        }

        return response;
    }

    public async Task<GetAllResponse> GetAllAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var response = new GetAllResponse();
        try
        {
            response.Products = await _productRepo.GetAllAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while getting products: {ex.Message}");
            response.Products = Enumerable.Empty<Product>();
            response.IsSuccess = false;
        }

        return response;
    }

    public async Task<GetByIdResponse> GetByIdAsync(int productId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var response = new GetByIdResponse();
        try
        {
            response.Product = await _productRepo.GetByIdAsync(productId, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while getting product by id: {ex.Message}");
            response.Product = default(Product);
            response.IsSuccess = false;
        }

        return response;
    }

    public async Task<CreateProductResponse> CreateAsync(CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var response = new CreateProductResponse() {Product = default(Product)};
        string imagePath = CombinePath(request.Image, request.FileName);
        bool successUpload = await UploadPhotoAsync(request.Image, imagePath);

        if (successUpload)
        {
            try
            {
                Product product = new Product()
                {
                    Name = request.Name,
                    Price = request.Price,
                    ImagePath = imagePath
                };
                var ProductShop = new ProductShop()
                {
                    Product = product,
                    ShopId = request.ShopId,
                    Quantity = request.Quantity
                };

                var productsAndTypes = CreateManyToMany(product, request.TypeIds);
                
                product.ProductsShops.Add(ProductShop);
                foreach (var el in productsAndTypes)
                {
                    product.ProductsWithTypes.Add(el);
                }
                
                await _productRepo.AddAsync(product, cancellationToken);
                response.Product = product;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                _logger.LogError($"Error while adding new product");
            }
        }
        else
        {
            response.IsSuccess = false;
        }

        return response;
    }

    private string CombinePath(IFormFile file, string fileName)
    {
        return Path.Combine(_environment.ContentRootPath, 
            @"wwwroot\Pictures", 
            $"{fileName}.{file.ContentType.Split("/")[1]}");
    }

    private async Task AddProdWithTypes(ProductsWithTypes[] arr, CancellationToken cancellationToken)
    {
        try
        {
            for (int i = 0; i < arr.Length; i++)
            {
                await _productsWithTypes.AddAsync(arr[i], cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while adding product with type: {ex.Message}");
        }
    }

    private ProductsWithTypes[] CreateManyToMany(Product product, List<int> typeIds)
    {
        var ProductsWithTypes = new ProductsWithTypes[typeIds.Count];
        for (int i = 0; i < typeIds.Count; i++)
        {
            ProductsWithTypes[i] = new ProductsWithTypes()
            {
                Product = product,
                TypeId = typeIds[i]
            };
        }
        return ProductsWithTypes;
    }

    private async Task<bool> UploadPhotoAsync(IFormFile file, string path)
    {
        bool result = true;

        try
        {
            using (var stream = new FileStream(path, FileMode.Create))
            {

                await file.CopyToAsync(stream);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while Setting Photo: {ex.Message}");
            result = false;
        }
        return result;
    }
}