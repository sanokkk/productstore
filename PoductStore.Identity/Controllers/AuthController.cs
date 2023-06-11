using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
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

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> LoginAsync([FromBody]LoginUserDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _userService.LoginAsync(model);
        if (result.Success)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result.Errors);
        }
    }
    
    

    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAsync()
    {
        var userId = User.Claims.First(f => f.Type == "id").Value;

        var result = await _userService.GetUserAsync(userId);

        if (result.Success)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest();
        }
    }
    
    
}