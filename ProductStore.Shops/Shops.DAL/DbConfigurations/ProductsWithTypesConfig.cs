using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductStore.Shops.Shops.Domain.Domain.ManyToManyModels;
using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shops.DAL.DbConfigurations;

public class ProductsWithTypesConfig: IEntityTypeConfiguration<ProductsWithTypes>
{
    public void Configure(EntityTypeBuilder<ProductsWithTypes> builder)
    {
        builder.ToTable("products_types");
        builder.HasKey(k => new { k.ProductId, k.TypeId });

        builder.HasOne<Product>(p => p.Product)
            .WithMany(m => m.ProductsWithTypes)
            .HasForeignKey(fk => fk.ProductId);
        builder.HasOne<ProductType>(pt => pt.ProductType)
            .WithMany(m => m.ProductsWithTypes)
            .HasForeignKey(fk => fk.TypeId);
    }
}