using Solimus.Domain.Interfaces;
using Solimus.Infrastructure.Persistence.Context;

namespace Solimus.Infrastructure.Persistence.Repositories;

public class UnitOfWork(SolimusContext context) : IUnitOfWork
{
    public async Task CommitAsync()
    {
        await context.SaveChangesAsync();
    }
}
