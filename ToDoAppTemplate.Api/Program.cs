using ToDoAppTemplate.Api.Infrastructure.Configuration;
using ToDoAppTemplate.Application.Infrastructure.Configuration;
using ToDoAppTemplate.Core.Infrastructure.Configuration;
using ToDoAppTemplate.Data.Infrastructure.Configuration;
using ToDoAppTemplate.Data.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpoints(builder.Configuration);
builder.Services.AddToDoApplication();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddToDoCore(builder.Configuration, builder.Environment);

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
