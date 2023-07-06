using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ProductStore.Shops.Shop.BLL.Services.Interfaces;

namespace ProductStore.Shops.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShopController: ControllerBase
{
    private readonly IShopService _shopService;
    private readonly IProductService _productService;
    private readonly ILogger<ShopController> _logger;

    public ShopController(IShopService shopService, ILogger<ShopController> logger, IProductService productService)
    {
        _shopService = shopService;
        _logger = logger;
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllShopsAsync(CancellationToken cancellationToken)
    {
        var response = await _shopService.GetShopsAsync(cancellationToken);
        if (response.IsSuccess)
        {
            _logger.LogInformation("Got shop's list");
            return Ok(response.Shops);
        }
        _logger.LogError("Error while getting shop's list");
        return BadRequest();
    }

    [HttpGet("{storeId:int}")]
    public async Task<IActionResult> GetProductsInStoreAsync([FromRoute]int storeId, CancellationToken cancellationToken)
    {
        var response = await _shopService.GetShopProductsAsync(storeId, cancellationToken);
        if (response.IsSuccess)
        {
            _logger.LogInformation("Got shop's products list");
            return Ok(response.Products);
        }
        return BadRequest();
    }
}