using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    public WeatherForecastController(IProductRepo productRepo)
    {
        _productRepo = productRepo;
    }

    [HttpGet]
    
    public async Task<IActionResult> GetAuth([FromQuery]int typeId ,CancellationToken cancellationToken)
    {
        var product = await _productRepo.GetByTypeAsync(typeId, cancellationToken);
        return Ok(product);
    }

    [HttpGet("Cat")]
    public IActionResult GetCategories()
    {
        return Ok();
    }
}