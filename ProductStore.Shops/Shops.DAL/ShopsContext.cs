using Microsoft.EntityFrameworkCore;
using ProductStore.Shops.Shops.DAL.DbConfigurations;
using ProductStore.Shops.Shops.Domain.Domain.ManyToManyModels;
using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shops.DAL;

public class ShopsContext: DbContext
{
    public ShopsContext(DbContextOptions<ShopsContext> options)
        : base(options)
    {
        //Database.EnsureCreated();
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<ProductsWithTypes> ProductsWithTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfig).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductTypeConfig).Assembly);
    }
}