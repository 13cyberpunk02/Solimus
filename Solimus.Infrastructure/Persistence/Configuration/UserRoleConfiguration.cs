using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Solimus.Infrastructure.Persistence.Configuration;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(new IdentityUserRole<string>
        {
            RoleId = "3f62b9a9-871c-47d3-b325-93d0b43ae8da",
            UserId = "05e2a611-ac9c-433a-af1e-ee6e2e393ccd"
        });
    }
}
