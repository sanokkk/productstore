using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductStore.Shops.Shops.Domain.Domain.ManyToManyModels;
using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shops.DAL.DbConfigurations;

public class ProductCardConfig: IEntityTypeConfiguration<ProductCard>
{
    public void Configure(EntityTypeBuilder<ProductCard> builder)
    {
        builder.ToTable("product_card");
        builder.HasKey(k => new { k.ProductId, k.CardId });
        builder.HasOne<Product>(pr => pr.Product)
            .WithMany(m => m.ProductsCards)
            .HasForeignKey(fk => fk.ProductId);
        builder.HasOne<Card>(c => c.Card)
            .WithMany(m => m.ProductsCards)
            .HasForeignKey(fk => fk.CardId);
    }
}