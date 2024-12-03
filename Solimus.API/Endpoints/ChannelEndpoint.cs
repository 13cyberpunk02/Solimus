using Carter;
using Microsoft.AspNetCore.Mvc;
using Solimus.API.Extensions;
using Solimus.Application.Interfaces;
using Solimus.Application.Models.Request.Channel;

namespace Solimus.API.Endpoints;

public class ChannelEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("channel/");
        group.MapGet("get-all-channels", GetAllChannels)
            .RequireAuthorization()
            .WithDescription("Получить список всех каналов");  
        group.MapGet("get-channel-by-id/{channelId:guid}", GetChannelById)
            .RequireAuthorization()
            .WithName("GetChannelById")
            .WithDescription("Получить канала по его Id");
        group.MapPost("create-channel", CreateChannel)
            .RequireAuthorization()
            .WithDescription("Создать канал");
    }

    private static async Task<IResult> GetAllChannels(IChannelService channelService)
    {
        var response = await channelService.GetAllChannels();
        
        return response.ToHttpResponse();
    }

    /// <summary>
    /// Получить канал по его Id.
    /// </summary>
    /// <param name="channelId">Идентификатор канала.</param>
    /// <param name="channelService">Сервис для работы с каналами</param>
    /// <returns>Данные канала.</returns>
    private static async Task<IResult> GetChannelById(IChannelService channelService, Guid channelId)
    {
        Console.WriteLine(channelId.ToString());
        var response = await channelService.GetChannelById(channelId);
        
        return response.ToHttpResponse();
    }
    /// <summary>
    /// Создание канала.
    /// </summary>
    /// <param name="request">Модель канала которая содержит информацию о канале.</param>
    /// <param name="channelService">Сервис для работы с каналами</param>
    /// <returns>Данные канала.</returns>
    private static async Task<IResult> CreateChannel(IChannelService channelService, CreateChannelRequest request)
    {
        var response = await channelService.CreateChannel(request);
        
        return response.ToHttpResponse();
    }
}
