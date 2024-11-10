using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solimus.Domain.Entities;

namespace Solimus.Infrastructure.Persistence.Configuration;

public class SolimusUserConfiguration : IEntityTypeConfiguration<SolimusUser>
{
    public void Configure(EntityTypeBuilder<SolimusUser> builder)
    {
        PasswordHasher<SolimusUser> passwordHasher = new();
        var user = new SolimusUser
        {
            Id = "05e2a611-ac9c-433a-af1e-ee6e2e393ccd",
            UserName = "Administrator",
            Email = "admin@domain.ru",
            EmailConfirmed = true
        };
        passwordHasher.HashPassword(user, "P@ssword1");

        builder.ToTable(nameof(SolimusUser));
        builder.HasData(user);
    }
}
