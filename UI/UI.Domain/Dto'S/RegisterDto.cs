using System.ComponentModel.DataAnnotations;

namespace UI.UI.Domain.Dto_S;

public class RegisterDto
{
    [Required]
    public string FullName { get; set; } = String.Empty;

    [Required]
    public string UserName { get; set; } = String.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = String.Empty;

    [Required]
    public string Password { get; set; } = String.Empty;

    [Required]
    [Compare(nameof(Password))]
    public string PasswordConfirm { get; set; } = String.Empty;
    
    [Required]
    [Range(minimum: 30000, maximum: 150000)]
    public double Salary { get; set; }
}