using Microsoft.AspNetCore.Mvc;
using Solimus.Application.Common;

namespace Solimus.API.Common.Extensions;

public static class ResultsExtension
{
    public static IResult ToHttpResponse(this Result result)
        => result.IsSuccess
            ? Results.NoContent()
            : result.Error.ToHttpError();

    public static IResult ToHttpResponse<T>(this Result<T> result)
        => result.IsSuccess
            ? Results.Ok(result.Value)
            : result.Error.ToHttpError();

    private static IResult ToHttpError(this Error error)
        => error.Code switch
        {
            ErrorTypes.Validation   => Results.BadRequest(CreateProblem(error)),
            ErrorTypes.BadRequest   => Results.BadRequest(CreateProblem(error)),
            ErrorTypes.NotFound     => Results.NotFound(CreateProblem(error)),
            ErrorTypes.Forbidden    => Results.Forbid(),
            ErrorTypes.Unauthorized => Results.Unauthorized(),
            _                       => Results.Problem(
                title: error.Code,
                detail: error.Message,
                statusCode: StatusCodes.Status500InternalServerError)
        };

    private static ProblemDetails CreateProblem(Error error)
        => new()
        {
            Title = error.Code,
            Detail = error.Message
        };
}