using UI.UI.Domain.Models;

namespace UI.Service.Interfaces;

public interface IProductService
{
    Task<Product> GetProductByIdAsync(int id);
}