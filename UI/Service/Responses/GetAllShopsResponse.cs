using UI.UI.Domain.Models;

namespace UI.Service.Responses;

public class GetAllShopsResponse: IBaseReponse
{
    public bool Success { get; set; } = true;

    public ICollection<Shop> Shops { get; set; }
}