﻿using DZarsky.ToDoAppTemplate.Domain.Common;
using DZarsky.ToDoAppTemplate.Domain.Todos;

namespace DZarsky.ToDoAppTemplate.Domain.Users;

public sealed class User : BaseEntity
{
    public string Login { get; set; } = string.Empty;
    
    public string Password { get; set; } = string.Empty;
    
    public string? Email { get; set; }
    
    public IList<Todo> Todos { get; set; } = new List<Todo>();
}