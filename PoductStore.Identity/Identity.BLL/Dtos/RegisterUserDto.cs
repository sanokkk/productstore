using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PoductStore.Identity.Identity.BLL.Dtos;

public class RegisterUserDto
{
    [Required]
    public string FullName { get; set; }
    
    [Required]
    public string UserName { get; set; }
    
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    [Compare(nameof(Password))]
    public string PasswordConfirm { get; set; }
    
    [Required] 
    public double Salary { get; set; }
}