using Solimus.Domain.Entities;
using Solimus.Domain.Interfaces;
using Solimus.Infrastructure.Persistence.Context;

namespace Solimus.Infrastructure.Persistence.Repositories;

public class ChannelRepository(SolimusContext context) : GenericRepository<Channel>(context), IChannelRepository
{
}
