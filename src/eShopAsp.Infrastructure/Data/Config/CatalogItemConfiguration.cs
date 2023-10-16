using eShopAsp.Core.Entities.CatalogItemAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopAsp.Infrastructure.Data.Config;

public class CatalogItemConfiguration : IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.ToTable("Catalog");
        builder.HasKey(ci => ci.Id);
        builder.Property(ci => ci.Name)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(ci => ci.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        builder.Property(ci => ci.PictureUri)
            .IsRequired(false);
        builder.HasOne(ci => ci.CatalogBrand)
            .WithMany()
            .HasForeignKey(ci => ci.CatalogBrandId);
        builder.HasOne(ci => ci.CatalogType)
            .WithMany()
            .HasForeignKey(ci => ci.CatalogTypeId);
    }
}