namespace PoductStore.Identity.Identity.BLL.Dtos;

public class RenewTokenRequestDto
{
    public string UserId { get; set; }
    public string RefreshToken { get; set; }
}