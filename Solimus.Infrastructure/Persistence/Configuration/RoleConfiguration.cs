using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Solimus.Infrastructure.Persistence.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        var adminRole = new IdentityRole
        {
            Id = "3f62b9a9-871c-47d3-b325-93d0b43ae8da",
            Name = "Administrator",
            NormalizedName = "Administrator".ToUpper()
        };

        builder.HasData(adminRole);
    }
}