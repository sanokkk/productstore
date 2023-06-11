using System.Net;

namespace UI.Service.Responses;

public class RegisterResponse: IBaseReponse<string>
{
    public bool Success { get; set; } = true;
    public List<string> Errors { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public string Content { get; set; }
}