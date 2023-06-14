using System.Net;

namespace UI.Service.Responses;

public class LoginResponse: IBaseReponse
{
    public bool Success { get; set; } = true;
    
    public string Message { get; set; }
    
    public DateTime? ExpireDate { get; set; }
    
    public string RefreshToken { get; set; }
}