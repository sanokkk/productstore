using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PoductStore.Identity.Identity.DAL.Models;

public class User: IdentityUser
{
    public string Password { get; set; } = "";

    public string FullName { get; set; } = "";

    public string ImagePath { get; set; } = "";

    public DateTime CreatedAt { get; set; }

    public double Balance { get; set; }
    
    public double Salary { get; set; }
    
    [Phone]
    public string PhoneNumber { get; set; } = "+7";
}