using DZarsky.ToDoAppTemplate.Api.Infrastructure.Configuration;
using DZarsky.ToDoAppTemplate.Application.Infrastructure.Configuration;
using DZarsky.ToDoAppTemplate.Core.Infrastructure.Configuration;
using DZarsky.ToDoAppTemplate.Data.Infrastructure.Configuration;
using DZarsky.ToDoAppTemplate.Data.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpoints(builder.Configuration);
builder.Services.AddToDoApplication();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddToDoCore(builder.Configuration);

var app = builder.Build();

#if DEBUG
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    db.Database.Migrate();
}
#endif

app.ConfigureToDoApi();

app.Run();
