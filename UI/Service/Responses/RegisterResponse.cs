using System.Net;

namespace UI.Service.Responses;

public class RegisterResponse: IBaseReponse
{
    public bool IsSuccess { get; set; } = true;
    public List<string> Errors { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}