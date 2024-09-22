using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solimus.Domain.Entities;

namespace Solimus.Infrastructure.Persistence.Configuration;

public class LogotypeConfiguration : IEntityTypeConfiguration<Logotype>
{
    public void Configure(EntityTypeBuilder<Logotype> builder)
    {
        builder.ToTable("Logotypes");

        builder.HasKey(logotype => logotype.Id);

        builder.Property(p => p.Uri)
            .IsRequired()
            .HasMaxLength(250)
            .HasColumnType("varchar(250)")
            .HasAnnotation("RegularExpression",
                @"^(http://|https://)?[a-z0-9]+([\-.]{1}[a-z0-9]+)\.[a-z]{2,5}(:[0-9]{1,5})?(/.)?$");

        builder.HasMany(c => c.Channels)
            .WithOne(l => l.Logotype)
            .HasForeignKey(k => k.LogotypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
