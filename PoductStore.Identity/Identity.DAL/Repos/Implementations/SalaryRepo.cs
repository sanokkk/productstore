using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PoductStore.Identity.Identity.DAL.Models;
using PoductStore.Identity.Identity.DAL.Repos.Interfaces;

namespace PoductStore.Identity.Identity.DAL.Repos.Implementations;

public class SalaryRepo: ISalaryRepo
{
    private readonly UserManager<User> _manager;
    private readonly UsersDbContext _context;

    public SalaryRepo(UserManager<User> manager, UsersDbContext context)
    {
        _manager = manager;
        _context = context;
    }

    public async Task GetSalary(double percent)
    {
        var users = await _manager.Users.ToArrayAsync();
        foreach (var user in users)
        {
            user.Balance += user.Salary * (percent / 100);
            await _context.SaveChangesAsync();
        }
    }
}