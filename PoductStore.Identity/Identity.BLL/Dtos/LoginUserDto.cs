using System.ComponentModel.DataAnnotations;

namespace PoductStore.Identity.Identity.BLL.Dtos;

public class LoginUserDto
{
    [Required]
    public string Username { get; init; }
    
    [Required]
    public string Password { get; init; }
}