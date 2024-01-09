using DZarsky.ToDoAppTemplate.Api.Infrastructure.Configuration;
using DZarsky.ToDoAppTemplate.Application.Infrastructure.Configuration;
using DZarsky.ToDoAppTemplate.Core.Infrastructure.Configuration;
using DZarsky.ToDoAppTemplate.Data.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpoints();
builder.Services.AddToDoApplication();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddToDoCore();

var app = builder.Build();

app.ConfigureToDoApi();

app.Run();
