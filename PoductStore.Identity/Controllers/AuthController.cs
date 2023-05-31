using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PoductStore.Identity.Identity.BLL.Dtos;
using PoductStore.Identity.Identity.BLL.Interfaces;

namespace PoductStore.Identity.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController: ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAsync([FromBody]RegisterUserDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var response = await _userService.RegisterUserAsync(model);

        if (response.Success)
        {
            Response.Cookies.Append("result", "success");
            return Ok(response);
        }
            
        return BadRequest(response.Errors);
    }
    
    

    [HttpGet]
    public IActionResult Get()
    {
        var response = _userService.GetUserNames();

        return Ok(response);
    }
    
    
}