using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sweets.Domain.Entities;

namespace Sweets.Infrastructure.Persistence.Configurations;

public class BoxConfiguration : IEntityTypeConfiguration<Box>
{
    public void Configure(EntityTypeBuilder<Box> builder)
    {
        builder.ToTable("boxes");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.NameAr).IsRequired().HasMaxLength(200);
        builder.Property(b => b.NameFr).IsRequired().HasMaxLength(200);
        builder.Property(b => b.ImageUrl).HasMaxLength(500);
        builder.Property(b => b.Capacity).IsRequired();
        builder.Property(b => b.PriceMode).IsRequired().HasConversion<int>();
        builder.Property(b => b.FixedPrice).HasPrecision(10, 3);
        builder.Property(b => b.SortOrder).IsRequired();
        builder.Property(b => b.IsActive).IsRequired();

        builder.HasIndex(b => b.IsActive);
    }
}