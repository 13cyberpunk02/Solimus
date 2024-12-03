using Solimus.Application.Common.Results;
using Solimus.Application.Error.ChannelError;
using Solimus.Application.Interfaces;
using Solimus.Application.Models.Request.Channel;
using Solimus.Application.Models.Response.Channel;
using Solimus.Application.Validators.Channel;
using Solimus.Domain.Entities;
using Solimus.Domain.Interfaces;

namespace Solimus.Application.Services;

public class ChannelService(IChannelRepository channelRepository, 
    CreateChannelRequestValidator channelRequestValidator,
    IUnitOfWork unitOfWork) : IChannelService
{
    public async Task<Result> GetAllChannels()
    {
        var channels = await channelRepository.GetAllAsync();
        if (!channels.Any())
            return Result.Success("Нет каналов в базе данных, создайте хотя бы одну");
        var result = channels.Select(channel => new GetChannelResponse(
            Id: channel.Id.ToString(),
            Name: channel.Name,
            CategoryId: channel.CategoryId.ToString(),
            LogotypeId: channel.LogotypeId.ToString(),
            Source: channel?.Source?.Uri
        )).ToList();
        return Result.Success(result);
    }

    public async Task<Result> GetChannelById(Guid channelId)
    {
        if(string.IsNullOrWhiteSpace(channelId.ToString()))
            return Result.Failure(ChannelErrors.ChannelNotFound);
        
        var channel = await channelRepository.GetById(channelId);
        if(channel == null)
            return Result.Failure(ChannelErrors.ChannelDoesntExist);
        
        return Result.Success(new GetChannelResponse(
            Id: channel.Id.ToString(),
            Name: channel.Name,
            CategoryId: channel.CategoryId.ToString(),
            LogotypeId: channel.LogotypeId.ToString(),
            Source: channel.Source?.Uri));
    }

    public async Task<Result> CreateChannel(CreateChannelRequest request)
    {
        var isValidModel = await channelRequestValidator.ValidateAsync(request);
        if(!isValidModel.IsValid)
            return Result.Failure(ChannelErrors.ListOfErrors(isValidModel.Errors.Select(x => x.ErrorMessage)));
        
        var isChannelExists = await channelRepository.IsChannelExist(request.Name);
        if(isChannelExists)
            return Result.Failure(ChannelErrors.ChannelExists);

        await channelRepository.AddAsync(new Channel { Name = request.Name });
        var result = await unitOfWork.CommitAsync();
        if (!result)
            return Result.Failure(ChannelErrors.ChannelAddError);
        return Result.Success("Канал успешно добавлен");
    }
    
    
}