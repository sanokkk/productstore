using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductStore.Shops.Shops.Domain.Domain.ManyToManyModels;
using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shops.DAL.DbConfigurations;

public class ProductShopConfig: IEntityTypeConfiguration<ProductShop>
{
    public void Configure(EntityTypeBuilder<ProductShop> builder)
    {
        builder.ToTable("product_shop");
        builder.HasKey(k => new { k.ProductId, k.ShopId });
        builder.Property(p => p.Quantity)
            .HasDefaultValue(0);
        builder.HasOne<Product>(pr => pr.Product)
            .WithMany(m => m.ProductsShops)
            .HasForeignKey(fk => fk.ProductId);
        builder.HasOne<Shop>(sh => sh.Shop)
            .WithMany(m => m.ProductsShops)
            .HasForeignKey(fk => fk.ShopId);
    }
}