using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Solimus.Infrastructure.Persistence.Context;

public class SolimusContextFactory : IDesignTimeDbContextFactory<SolimusContext>
{
    public SolimusContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddUserSecrets<SolimusContextFactory>()
            .Build();

        var connectionString = configuration.GetConnectionString("SqlServerConnection");

        var optionsBuilder = new DbContextOptionsBuilder<SolimusContext>();
        optionsBuilder.UseSqlServer(connectionString);
        return new SolimusContext(optionsBuilder.Options);           
    }
}
