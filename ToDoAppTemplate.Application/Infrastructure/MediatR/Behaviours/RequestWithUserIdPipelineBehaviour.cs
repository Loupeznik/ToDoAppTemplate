using ToDoAppTemplate.Domain.Common;
using ToDoAppTemplate.Domain.Common.MediatR;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ToDoAppTemplate.Application.Infrastructure.MediatR.Behaviours;

public class RequestWithUserIdPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IAuthenticatedRequest
    where TResponse : IMediatrBaseResult
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RequestWithUserIdPipelineBehaviour(IHttpContextAccessor httpContextAccessor) =>
        _httpContextAccessor = httpContextAccessor;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var idClaim = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == "UserID");
        _ = int.TryParse(idClaim?.Value, out var userId);

        if (userId > 0)
        {
            request.UserId = userId;
        }

        return await next();
    }
}
