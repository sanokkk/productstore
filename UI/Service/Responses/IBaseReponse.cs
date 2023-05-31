using System.Net;

namespace UI.Service.Responses;

public interface IBaseReponse
{
    bool IsSuccess { get; set; }
    
    List<string> Errors { get; set; }
    
    HttpStatusCode StatusCode { get; set; }
}