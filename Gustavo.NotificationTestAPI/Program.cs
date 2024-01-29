using Gustavo.NotificationTestAPI.Middlewares;
using Gustavo.NotificationTestAPI.Model;
using Gustavo.NotificationTestAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<DbSession>();
builder.Services.AddTransient<INotificationRepository, NotificationRepository>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.Run();
