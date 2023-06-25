using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductStore.Shops.Shops.Domain.Domain.ManyToManyModels;
using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shops.DAL.DbConfigurations;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");
        builder.HasKey(k => k.Id);
        builder.HasMany<ProductType>(pt => pt.ProductTypes)
            .WithMany(pr => pr.Products)
            .UsingEntity<ProductsWithTypes>(j =>
            {
                j.HasOne<Product>(pr => pr.Product)
                    .WithMany(pt => pt.ProductsWithTypes)
                    .HasForeignKey(fk => fk.ProductId);
                j.HasOne<ProductType>(pt => pt.ProductType)
                    .WithMany(pr => pr.ProductsWithTypes)
                    .HasForeignKey(fk => fk.TypeId);
                j.HasKey(k => new { k.ProductId, k.TypeId });
                j.ToTable("products_types");
            });
    }
}