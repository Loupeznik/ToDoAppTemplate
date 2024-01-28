using ToDoAppTemplate.Api.Infrastructure.Resolvers;
using ToDoAppTemplate.Domain.Common.MediatR;
using FastEndpoints;

namespace ToDoAppTemplate.Api.Infrastructure.Extensions;

internal static class EndpointExtensions
{
    internal static Task ResolveResult<TResult, TResponse>(this IEndpoint endpoint, TResult result, TResponse response,
        CancellationToken ct = default)
        where TResult : IMediatrBaseResult where TResponse : class
    {
        endpoint.HttpContext.MarkResponseStart();

        var errors = new Dictionary<string, List<string>>();

        if (result.Errors.Any())
        {
            errors = result.Errors
                           .GroupBy(x => x.Key, x => x.Message)
                           .ToDictionary(x => x.Key, x => x.ToList())!;
        }

        if (!result.IsSuccess)
        {
            return endpoint.HttpContext.Response.SendAsync(new ErrorResponse
            {
                StatusCode = result.Status.Resolve(),
                Message = result.Message ?? "An error has occured during the request",
                Errors = errors
            }, result.Status.Resolve(), cancellation: ct);
        }

        return endpoint.HttpContext.Response.SendAsync(response, result.Status.Resolve(), cancellation: ct);
    }

    internal static Task ResolveResult<TResult>(this IEndpoint endpoint, TResult result, CancellationToken ct = default)
        where TResult : IMediatrBaseResult
    {
        endpoint.HttpContext.MarkResponseStart();

        var errors = new Dictionary<string, List<string>>();

        if (result.Errors.Any())
        {
            errors = result.Errors
                           .GroupBy(x => x.Key, x => x.Message)
                           .ToDictionary(x => x.Key.ToLowerInvariant(), x => x.ToList())!;
        }

        if (!result.IsSuccess)
        {
            return endpoint.HttpContext.Response.SendAsync(new ErrorResponse
            {
                StatusCode = result.Status.Resolve(),
                Message = result.Message ?? "One or more errors occurred!",
                Errors = errors
            }, result.Status.Resolve(), cancellation: ct);
        }

        return SendEmptyResponse(endpoint, result.Status.Resolve(), ct, true);
    }

    public static Task SendEmptyResponse(this IEndpoint endpoint, int statusCode, CancellationToken ct = default,
        bool? preventResponseStart = false)
    {
        if (!preventResponseStart.GetValueOrDefault())
        {
            endpoint.HttpContext.MarkResponseStart();
        }

        endpoint.HttpContext.Response.StatusCode = statusCode;

        return endpoint.HttpContext.Response.StartAsync(ct);
    }
}
