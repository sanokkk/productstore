using MassTransit;
using ProductStore.Contracts;

namespace ProductStore.Shops.Consumer;

public class ProduStockConsumer: IConsumer<ProductStockContract>
{
    private readonly ILogger<ProduStockConsumer> _logger;

    public ProduStockConsumer(ILogger<ProduStockConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<ProductStockContract> context)
    {
        var quantity = context.Message.quantity;
        _logger.LogInformation($"Products from Queue: {quantity}");
        return Task.CompletedTask;
    }
}