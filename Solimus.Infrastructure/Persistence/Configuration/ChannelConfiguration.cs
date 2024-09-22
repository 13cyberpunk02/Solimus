using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solimus.Domain.Entities;

namespace Solimus.Infrastructure.Persistence.Configuration;

public class ChannelConfiguration : IEntityTypeConfiguration<Channel>
{
    public void Configure(EntityTypeBuilder<Channel> builder)
    {
        builder.ToTable("Channels");

        builder.HasKey(channel => channel.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(30)
            .HasColumnType("varchar(30)");

        builder.HasOne(c => c.Source)
            .WithOne(s => s.Channel)
            .HasForeignKey<Source>(k => k.ChannelId);
    }
}
