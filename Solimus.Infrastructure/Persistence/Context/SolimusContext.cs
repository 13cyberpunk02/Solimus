using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Solimus.Domain.Entities;

namespace Solimus.Infrastructure.Persistence.Context;

public class SolimusContext(DbContextOptions<SolimusContext> options) : IdentityDbContext<SolimusUser>(options)
{
    public DbSet<Channel> Channels { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Logotype> Logotypes { get; set; }
    public DbSet<Source> Sources { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(SolimusContext).Assembly);
        base.OnModelCreating(builder);
    }
}
