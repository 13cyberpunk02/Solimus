using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solimus.Domain.Entities;

namespace Solimus.Infrastructure.Persistence.Configuration;

public class SourceConfiguration : IEntityTypeConfiguration<Source>
{
    public void Configure(EntityTypeBuilder<Source> builder)
    {
        builder.ToTable("Sources");

        builder.HasKey(source => source.Id);

        builder.Property(p => p.Uri)
            .IsRequired()
            .HasMaxLength(250)
            .HasColumnType("nvarchar(250)")
            .HasAnnotation("RegularExpression",
                @"^(http://|https://)?[a-z0-9]+([\-.]{1}[a-z0-9]+)\.[a-z]{2,5}(:[0-9]{1,5})?(/.)?$");
    }
}
