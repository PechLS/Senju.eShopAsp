using eShopAsp.Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopAsp.Infrastructure.Data.Config;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.OwnsOne(i => i.ItemOrdered, io =>
        {
            io.WithOwner();
            io.Property(cio => cio.ProductName)
                .HasMaxLength(50)
                .IsRequired();
        });
        builder.Property(oi => oi.UnitPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
    }
}