using System.Net;

namespace UI.Service.Responses;

public class GetResponse: IBaseReponse
{
    public bool Success { get; set; }
    public MyUser Content { get; set; }
}

public class MyUser
{
    public string Id { get; set; } = string.Empty;

    public string UserName { get; set; } = String.Empty;
    
    public string Email { get; set; } = String.Empty;
}