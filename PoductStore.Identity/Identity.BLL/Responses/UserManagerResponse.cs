namespace PoductStore.Identity.Identity.BLL.Responses;

public class UserManagerResponse
{
    public IEnumerable<string> Errors { get; set; }

    public bool Success { get; set; } = true;
    
    public string Message { get; set; }
    
}