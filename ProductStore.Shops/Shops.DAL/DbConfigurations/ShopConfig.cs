using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shops.DAL.DbConfigurations;

public class ShopConfig: IEntityTypeConfiguration<Domain.Domain.Models.Shop>
{
    public void Configure(EntityTypeBuilder<Domain.Domain.Models.Shop> builder)
    {
        builder.ToTable("shops");
        builder.HasKey(k => k.Id);
    }
}