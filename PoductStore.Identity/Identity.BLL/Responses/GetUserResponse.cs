using PoductStore.Identity.Identity.DAL.Models;

namespace PoductStore.Identity.Identity.BLL.Responses;

public class GetUserResponse
{
    public bool Success { get; set; } = true;
    
    public UserForResponse Content { get; set; }
    
    
}

public class UserForResponse
{
    public string Id { get; set; } = string.Empty;

    public string UserName { get; set; } = String.Empty;
    
    public string Email { get; set; } = String.Empty;
    
    public double Balance { get; set; }

    public static UserForResponse MapToDto(User user)
    {
        return new UserForResponse()
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Balance = user.Balance
        };
    }
}