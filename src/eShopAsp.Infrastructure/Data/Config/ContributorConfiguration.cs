using eShopAsp.Core.Entities.ContributorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopAsp.Infrastructure.Data.Config;

public class ContributorConfiguration : IEntityTypeConfiguration<Contributor>
{
    public void Configure(EntityTypeBuilder<Contributor> builder)
    {
        builder.Property(p => p.Name)
            .HasMaxLength(DataSchemeConstants.DEFAULT_NAME_LENGTH)
            .IsRequired();
        builder.Property(x => x.Status)
            .HasConversion(
                x => x.Value,
                x => ContributorStatus.FromValue(x));
    }
}