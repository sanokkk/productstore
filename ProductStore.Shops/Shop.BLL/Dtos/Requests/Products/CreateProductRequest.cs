namespace ProductStore.Shops.Shop.BLL.Dtos.Requests.Products;

public class CreateProductRequest
{
    public string Name { get; set; }
    
    public double Price { get; set; }
    
    public string FileName { get; set; }
    
    public IFormFile Image { get; set; }

    public List<int> TypeIds { get; set; } = new();
    
    public int ShopId { get; set; }
    
    public int Quantity { get; set; }
}