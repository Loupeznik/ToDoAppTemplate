﻿using DZarsky.ToDoAppTemplate.Api.Common.Models;
using DZarsky.ToDoAppTemplate.Domain.Todos;

namespace DZarsky.ToDoAppTemplate.Api.Todos.Models.Responses;

public class GetToDoResponse : GetResourceByIdRequest
{
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsComplete { get; set; }

    public DateTime? DateCompleted { get; set; }

    internal static GetToDoResponse MapFromToDo(Todo todo)
    {
        return new GetToDoResponse
        {
            Id = todo.Id,
            DateCompleted = todo.DateCompleted,
            Description = todo.Description,
            Title = todo.Title,
            IsComplete = todo.IsComplete
        };
    }
}
