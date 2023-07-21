namespace PoductStore.Identity.Identity.DAL.Repos.Interfaces;

public interface ISalaryRepo
{
    Task GetSalary(double percent);
}