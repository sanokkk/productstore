using Microsoft.AspNetCore.Mvc;
using ProductStore.Shops.Shop.BLL.Dtos.Requests.Products;
using ProductStore.Shops.Shop.BLL.Services.Interfaces;

namespace ProductStore.Shops.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController: ControllerBase
{
    private readonly IProductService _service;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IProductService service, ILogger<ProductController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var response = await _service.GetAllAsync(cancellationToken);
        if (response.IsSuccess)
        {
            return Ok(response.Products);    
        }
        _logger.LogError("Bad request while getting products");
        return BadRequest();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute]int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var response = await _service.GetByIdAsync(id, cancellationToken);
        if (response.IsSuccess)
        {
            return Ok(response.Product);
        }
        _logger.LogError($"Bad request while getting product by id {id}");
        return BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm]CreateProductRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok();
    }
}