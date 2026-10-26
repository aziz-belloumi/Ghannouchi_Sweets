using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sweets.Domain.Entities;

namespace Sweets.Infrastructure.Persistence.Configurations;

public class SweetConfiguration : IEntityTypeConfiguration<Sweet>
{
    public void Configure(EntityTypeBuilder<Sweet> builder)
    {
        builder.ToTable("sweets");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.NameAr).IsRequired().HasMaxLength(200);
        builder.Property(s => s.NameFr).IsRequired().HasMaxLength(200);
        builder.Property(s => s.ImageUrl).HasMaxLength(500);
        builder.Property(s => s.PricePerPiece).HasPrecision(10, 3);
        builder.Property(s => s.IsActive).IsRequired();

        builder.HasOne(s => s.Category)
               .WithMany()
               .HasForeignKey(s => s.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(s => s.CategoryId);
        builder.HasIndex(s => s.IsActive);
    }
}