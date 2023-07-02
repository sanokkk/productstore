namespace ProductStore.Exceptions.Exceptions.HTTP;

public class NotSuccessResponseException: Exception
{
    public NotSuccessResponseException()
    :base("HTTP Response was not successful")
    {
        
    }
}