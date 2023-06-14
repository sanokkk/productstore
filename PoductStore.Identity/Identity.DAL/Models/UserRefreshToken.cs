using System.ComponentModel.DataAnnotations.Schema;

namespace PoductStore.Identity.Identity.DAL.Models;

public class UserRefreshToken
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("token")]
    public string Token { get; set; }
    
    [Column("userid")]
    public string UserId { get; set; }
    
    [Column("expirationdate")]
    public DateTime ExpirationDate { get; set; }
}