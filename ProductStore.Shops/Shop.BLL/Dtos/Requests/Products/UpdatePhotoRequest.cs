namespace ProductStore.Shops.Shop.BLL.Dtos.Requests.Products;

public record UpdatePhotoRequest(IFormFile File, int productId, string FileName);