using Factory.Factories;
using MassTransit;
using ProductStore.Contracts;

namespace Factory.Publishers;

public class ProductStockPublisher: BackgroundService
{
    private readonly IBus _bus;
    private readonly ILogger<ProductStockPublisher> _logger;

    public ProductStockPublisher(IBus bus, ILogger<ProductStockPublisher> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var prodStock = ProductStockFactory.CreateProduct();

            await _bus.Publish<ProductStockContract>(prodStock, stoppingToken);
            _logger.LogInformation("Sended products to stocks");

            await Task.Delay(TimeSpan.FromHours(5), stoppingToken);
        }
    }
}