using Solimus.Domain.Interfaces;
using Solimus.Infrastructure.Data.Context;

namespace Solimus.Infrastructure.Data.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private bool _disposed = false;
    public IUserRepository Users => field is null ? field ??= new UserRepository(context) : field;
    public IRoleRepository Roles => field is null ? field ??= new RoleRepository(context) : field;
    public IUserRoleRepository UserRoles =>  field is null ? field ??= new UserRoleRepository(context) : field;
    public IRefreshTokenRepository RefreshTokens => field is null ? field ??= new RefreshTokenRepository(context) : field;
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await context.SaveChangesAsync(cancellationToken);
    
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            context.Dispose();
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}