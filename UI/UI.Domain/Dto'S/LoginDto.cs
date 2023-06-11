using System.ComponentModel.DataAnnotations;

namespace UI.UI.Domain.Dto_S;

public class LoginDto
{
    [Required]
    public string Username { get; set; }
    
    [Required]
    public string Password { get; set; }
}