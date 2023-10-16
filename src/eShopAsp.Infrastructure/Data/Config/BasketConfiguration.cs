using eShopAsp.Core.Entities.BasketAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopAsp.Infrastructure.Data.Config;

public class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        var navigation = builder.Metadata.FindNavigation(nameof(Basket.Items));
        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
        builder.Property(b => b.BuyerId)
            .HasMaxLength(256)
            .IsRequired();
    }
}