﻿namespace ToDoAppTemplate.Api.Auth.Models;

public class LoginRequest
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
