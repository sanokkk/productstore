using ProductStore.Contracts;

namespace Factory.Factories;

public static class ProductStockFactory
{
    public static ProductStockContract CreateProduct()
    {
        var rand = new Random();
        var quantity = rand.Next(10, 100);
        return new ProductStockContract(quantity);
    } 
}