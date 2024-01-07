using DZarsky.ToDoAppTemplate.Domain.Common.MediatR;
using FastEndpoints;

namespace DZarsky.ToDoAppTemplate.Api.Infrastructure.Extensions;

internal static class EndpointExtensions
{
    internal static Task ResolveResult<TResult, TResponse>(this IEndpoint endpoint, TResult result, TResponse response, CancellationToken ct = default) 
        where TResult : IMediatrBaseResult where TResponse: class
    {
        endpoint.HttpContext.MarkResponseStart();

        if (!result.IsSuccess)
        {
            return endpoint.HttpContext.Response.SendAsync(new ErrorResponse
            {
                Message = result.Message ?? "An error has occured during the request", 
            }, 400, cancellation: ct); // todo: status code and response body
        }
        
        return endpoint.HttpContext.Response.SendAsync(response, 200, cancellation: ct); // todo: status code
    }

    internal static Task SendEmptyResponse(this IEndpoint endpoint, int statusCode, CancellationToken ct = default)
    {
        endpoint.HttpContext.MarkResponseStart();

        endpoint.HttpContext.Response.StatusCode = statusCode;

        return endpoint.HttpContext.Response.StartAsync(ct);
    }
}
