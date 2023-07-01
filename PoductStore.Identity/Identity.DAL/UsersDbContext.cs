using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PoductStore.Identity.Identity.DAL.Models;

namespace PoductStore.Identity.Identity.DAL;

public class UsersDbContext: IdentityDbContext
{
    public UsersDbContext(DbContextOptions<UsersDbContext> options)
    :base(options)
    {
        //Database.EnsureCreated();
    }
    public DbSet<User> Users { get; set; }
    
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
}