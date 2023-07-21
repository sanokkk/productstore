using MassTransit;
using ProductStore.Contracts;
using ProductStore.Shops.Shops.DAL.Repositories.Interfaces.Consumer;

namespace ProductStore.Shops.Consumer;

public class ProduStockConsumer: IConsumer<ProductStockContract>
{
    private readonly ILogger<ProduStockConsumer> _logger;
    private readonly IProductStockRepo _productStock;

    public ProduStockConsumer(ILogger<ProduStockConsumer> logger, IProductStockRepo productStock)
    {
        _logger = logger;
        _productStock = productStock;
    }

    public async Task Consume(ConsumeContext<ProductStockContract> context)
    {
        var quantity = context.Message.quantity;
        try
        {
            await _productStock.IncreaseStock(quantity);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while adding products to shop: {ex.Message}");

        }
        _logger.LogInformation($"Products from Queue: {quantity}");
    }
}