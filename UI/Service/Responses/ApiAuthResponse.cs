namespace UI.Service.Responses;

public class ApiAuthResponse
{
    public IEnumerable<string> Errors { get; set; }

    public bool Success { get; set; } = true;
    
    public string Message { get; set; }
    
    public DateTime? ExpireDate { get; set; }
}