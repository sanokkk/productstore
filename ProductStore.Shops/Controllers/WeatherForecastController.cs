using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductStore.Shops.Shops.DAL;
using ProductStore.Shops.Shops.Domain.Domain.ManyToManyModels;
using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ShopsContext _context;

    public WeatherForecastController(ShopsContext context)
    {
        _context = context;
    }

    [HttpGet]
    
    public IActionResult GetAuth()
    {
        var product = _context.Products
            .Include(i => i.ProductsWithTypes)
            .ThenInclude(ti => ti.ProductType)
            .ToArray();
        return Ok(product);
    }

    [HttpGet("Cat")]
    public IActionResult GetCategories()
    {
        return Ok();
    }
}