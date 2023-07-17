namespace UI.Service.Responses;

public class GetProductQuantityResponse
{
    public bool IsSuccess { get; set; } = true;
    public Dictionary<int, int> ProductQuantity { get; set; }
}