using Solimus.Domain.Entities;

namespace Solimus.Domain.Interfaces;

public interface IChannelRepository : IGenericRepository<Channel>
{
    Task<bool> IsChannelExist(string name);
}
