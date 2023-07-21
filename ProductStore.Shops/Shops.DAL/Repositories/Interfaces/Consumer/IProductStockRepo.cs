namespace ProductStore.Shops.Shops.DAL.Repositories.Interfaces.Consumer;

public interface IProductStockRepo
{
    Task IncreaseStock(int quantity);
}