using ProductStore.Contracts;

namespace Factory.Factories;

public static class SalaryFactory
{
    public static SalaryContract CreateSalary()
    {
        var rand = new Random();
        var salaryPercent = rand.NextDouble() * 100;
        return new SalaryContract(salaryPercent);
    }
}