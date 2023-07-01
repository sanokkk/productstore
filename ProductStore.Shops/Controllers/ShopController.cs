using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ProductStore.Shops.Shop.BLL.Services.Interfaces;

namespace ProductStore.Shops.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShopController: ControllerBase
{
    private readonly IShopService _shopService;
    private readonly ILogger<ShopController> _logger;

    public ShopController(IShopService shopService, ILogger<ShopController> logger)
    {
        _shopService = shopService;
        _logger = logger;
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
}