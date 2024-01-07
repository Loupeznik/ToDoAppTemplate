using DZarsky.ToDoAppTemplate.Api.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpoints();

var app = builder.Build();

app.ConfigureToDoApi();

app.Run();
