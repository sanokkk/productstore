using System.Net;

namespace UI.Service.Responses;

public class LoginResponse: IBaseReponse<string>
{
    public bool Success { get; set; } = true;
    
    public string Content { get; set; } = String.Empty;
}