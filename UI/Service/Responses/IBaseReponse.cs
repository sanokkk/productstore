using System.Net;

namespace UI.Service.Responses;

public interface IBaseReponse<T>
{
    bool Success { get; set; }
    T Content { get; set; }
}