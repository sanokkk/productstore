using MassTransit;
using PoductStore.Identity.Identity.DAL.Repos.Interfaces;
using ProductStore.Contracts;

namespace PoductStore.Identity.Consumers;

public class SalaryConsumer: IConsumer<SalaryContract>
{
    private readonly ISalaryRepo _salaryRepo;
    private readonly ILogger<SalaryConsumer> _logger;

    public SalaryConsumer(ILogger<SalaryConsumer> logger, ISalaryRepo salaryRepo)
    {
        _logger = logger;
        _salaryRepo = salaryRepo;
    }

    public async Task Consume(ConsumeContext<SalaryContract> context)
    {
        var percent = context.Message.percent;
        try
        {
            await _salaryRepo.GetSalary(percent);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while getting salary: {ex.Message}");
        }
        _logger.LogInformation("Got salary");
    }
}