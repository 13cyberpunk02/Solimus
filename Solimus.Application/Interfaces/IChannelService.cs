using Solimus.Application.Common.Results;
using Solimus.Application.Models.Request.Channel;

namespace Solimus.Application.Interfaces;

public interface IChannelService
{
    Task<Result> GetAllChannels();
    Task<Result> GetChannelById(Guid channelId);
    Task<Result> CreateChannel(CreateChannelRequest request);
}