using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductStore.Shops.Shop.BLL.Dtos.Requests.Products;
using ProductStore.Shops.Shop.BLL.Dtos.Responses.Products;
using ProductStore.Shops.Shop.BLL.Services.Interfaces;
using ProductStore.Shops.Shops.DAL;
using ProductStore.Shops.Shops.DAL.Repositories.Interfaces;
using ProductStore.Shops.Shops.Domain.Domain.ManyToManyModels;
using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IProductRepo _productRepo;
    private readonly IProductService _productService;

    public WeatherForecastController(IProductRepo productRepo, IProductService productService)
    {
        _productRepo = productRepo;
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAuth(CancellationToken cancellationToken)
    {
        var product = await _productRepo.GetByIdAsync(1, cancellationToken);
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> GetCategories([FromForm] UpdatePhotoRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var response = await _productService.UpdatePhotoAsync(request, cancellationToken);
        if (response.IsSuccess)
        {
            return Ok(await _productRepo.GetByIdAsync(1, cancellationToken));
        }
        return BadRequest();
    }

    [HttpPost("File")]
    public IActionResult CheckFile([FromForm] IFormFile File)
    {
        return Ok(File.Name);
    }
}