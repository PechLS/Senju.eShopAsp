using eShopAsp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopAsp.Infrastructure.Data.Config;

public class CatalogTypeConfiguration : IEntityTypeConfiguration<CatalogType>
{
    public void Configure(EntityTypeBuilder<CatalogType> builder)
    {
        builder.HasKey(ct => ct.Id);
        builder.Property(ct => ct.Type)
            .IsRequired()
            .HasMaxLength(100);
    }
}