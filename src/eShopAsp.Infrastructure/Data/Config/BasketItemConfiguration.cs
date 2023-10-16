using eShopAsp.Core.Entities.BasketAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopAsp.Infrastructure.Data.Config;

public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        builder.Property(bi => bi.UnitPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
    }
}