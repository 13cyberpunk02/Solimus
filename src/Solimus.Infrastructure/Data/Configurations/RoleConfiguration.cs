using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solimus.Domain.Entities;

namespace Solimus.Infrastructure.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");
        
        builder.HasKey(r => r.RoleId);
        builder.HasIndex(r => r.RoleId)
            .IsUnique();
        
        builder.HasIndex(r => r.RoleName)
            .IsUnique();
        builder.Property(r => r.RoleName)
            .IsRequired()
            .HasMaxLength(50);
    }
}