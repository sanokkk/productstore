using Factory.Factories;
using MassTransit;

namespace Factory.Publishers;

public class SalaryPublisher: BackgroundService
{
    private readonly IBus _bus;

    public SalaryPublisher(IBus bus)
    {
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var salary = SalaryFactory.CreateSalary();
            await _bus.Publish(salary, stoppingToken);

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}