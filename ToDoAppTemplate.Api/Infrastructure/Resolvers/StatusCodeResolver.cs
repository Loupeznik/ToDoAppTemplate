using ToDoAppTemplate.Core.Auth.Models;
using ToDoAppTemplate.Domain.Common.Results;

namespace ToDoAppTemplate.Api.Infrastructure.Resolvers;

internal static class StatusCodeResolver
{
    internal static int Resolve<TResultType>(this TResultType status)
    {
        return status switch
        {
            AuthResultStatus authResultStatus => ResolveAuthResult(authResultStatus),
            ResultStatus resultStatus => ResolveGeneralResult(resultStatus),
            _ => StatusCodes.Status200OK
        };
    }
    
    private static int ResolveAuthResult(AuthResultStatus status)
    {
        return status switch
        {
            AuthResultStatus.Success => StatusCodes.Status200OK,
            AuthResultStatus.InvalidLoginOrPassword => StatusCodes.Status401Unauthorized,
            AuthResultStatus.UserInactive => StatusCodes.Status401Unauthorized,
            AuthResultStatus.Error => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status200OK,
        };
    }

    private static int ResolveGeneralResult(ResultStatus status)
    {
        return status switch
        {
            ResultStatus.Success => StatusCodes.Status200OK,
            ResultStatus.EntityNotFound => StatusCodes.Status404NotFound,
            ResultStatus.Unauthorized => StatusCodes.Status401Unauthorized,
            ResultStatus.AlreadyExists => StatusCodes.Status409Conflict,
            ResultStatus.ValidationError => StatusCodes.Status400BadRequest,
            ResultStatus.EntityCreated => StatusCodes.Status201Created,
            ResultStatus.InternalError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status200OK,
        };
    }
}
