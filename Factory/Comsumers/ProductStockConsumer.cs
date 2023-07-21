using MassTransit;
using ProductStore.Contracts;

namespace Factory.Comsumers;

public class ProductStockConsumer : IConsumer<ProductStockContract>
{
	private readonly ILogger<ProductStockConsumer> _logger;

	public ProductStockConsumer(ILogger<ProductStockConsumer> logger)
	{
		_logger = logger;
	}

	public Task Consume(ConsumeContext<ProductStockContract> context)
	{
		var message = context.Message.quantity;
		_logger.LogInformation($"Got message from factory: {message}");
		return Task.CompletedTask;
	}
}
