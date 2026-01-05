using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solimus.Domain.Entities;

namespace Solimus.Infrastructure.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    private readonly List<Role> _roles =
    [
        new Role
        {
            RoleId = Guid.Parse("83CF0C0F-D889-4332-8F7F-258CB9A737A4"),
            RoleName = "Admin"
        },
        new Role
        {
            RoleId = Guid.Parse("56EEEE83-A801-43DC-983E-C7A28623B254"),
            RoleName = "Manager"
        },
        new Role
        {
            RoleId = Guid.Parse("FBC57B00-43C3-498E-86B0-77B57778A296"),
            RoleName = "User"
        }
    ];
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
        
        
        builder.HasData(_roles);
    }
}