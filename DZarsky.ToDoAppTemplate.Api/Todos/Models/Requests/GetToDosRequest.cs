﻿using DZarsky.ToDoAppTemplate.Api.Common.Models;
using DZarsky.ToDoAppTemplate.Domain.Common;
using FastEndpoints;

namespace DZarsky.ToDoAppTemplate.Api.Todos.Models.Requests;

public sealed class GetToDosRequest : PagedRequest, IAuthenticatedRequest
{
    [FromClaim]
    public int? UserId { get; set; }
    
    public bool IncludeCompleted { get; init; }
    
    public bool IncludeDeleted { get; init; }
}
