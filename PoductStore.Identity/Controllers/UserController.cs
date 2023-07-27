using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoductStore.Identity.Identity.BLL.Interfaces;

namespace PoductStore.Identity.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Ping()
    {
        return Ok();
    }


    [HttpPost]
    public async Task<IActionResult> CheckPay([FromBody]double price, CancellationToken cancellationToken)
    {
        var userid = User.Claims.First(x => x.Type == "id").Value;

        var result = await _userService.DecreaseBalance(userid, price, cancellationToken);
        if (result)
            return Ok();
        return BadRequest();
    }
}