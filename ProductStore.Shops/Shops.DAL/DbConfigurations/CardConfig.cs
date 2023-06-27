using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shops.DAL.DbConfigurations;

public class CardConfig: IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.ToTable("cards");
        builder.HasKey(k => k.Id);
    }
}