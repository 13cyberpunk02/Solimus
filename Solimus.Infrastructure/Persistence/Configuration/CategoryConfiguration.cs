using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solimus.Domain.Entities;

namespace Solimus.Infrastructure.Persistence.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(category => category.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(30)
            .HasColumnType("varchar(30)");

        builder.HasMany(c => c.Channels)
            .WithOne(c => c.Category)
            .HasForeignKey(k => k.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
