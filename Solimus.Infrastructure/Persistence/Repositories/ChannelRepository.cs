using Microsoft.EntityFrameworkCore;
using Solimus.Domain.Entities;
using Solimus.Domain.Interfaces;
using Solimus.Infrastructure.Persistence.Context;

namespace Solimus.Infrastructure.Persistence.Repositories;

public class ChannelRepository(SolimusContext context) : GenericRepository<Channel>(context), IChannelRepository
{
    private readonly SolimusContext _context = context;

    public async Task<bool> IsChannelExist(string name)
    {
        var channel = await _context.Channels.FirstOrDefaultAsync(c => c.Name == name);
        return channel is not null;
    }

    public new async Task<List<Channel>> GetAllAsync()
    {
        var channels = await _context.Channels
            .Include(c => c.Source)
            .Include(c => c.Category)
            .Include(c => c.Logotype)
            .ToListAsync();
        return channels;
    }

    public new async Task<Channel?> GetById(Guid id)
    {
        var channel = await _context.Channels
                .Include(c => c.Source)
                .Include(c => c.Category)
                .Include(c => c.Logotype)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        return channel;
    }
}
