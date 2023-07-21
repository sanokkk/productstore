using Factory.Factories;
using MassTransit;
using ProductStore.Contracts;

namespace Factory.Publishers;

public class SalaryPublisher: BackgroundService
{
    private readonly IBus _bus;
    private readonly ILogger<SalaryPublisher> _logger;

    public SalaryPublisher(IBus bus, ILogger<SalaryPublisher> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("salary publish");
            var salary = SalaryFactory.CreateSalary();
            await _bus.Publish<SalaryContract>(salary, stoppingToken);

            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
}