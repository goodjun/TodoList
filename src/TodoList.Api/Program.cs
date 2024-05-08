using TodoList.Api.Data;
using Microsoft.EntityFrameworkCore;
using TodoList.Api.DbUp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// mysql service
var connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:DBConnectionString");
var serverVersion = new MySqlServerVersion(new Version(5, 7, 44));
builder.Services.AddDbContext<TodoListDbContext>(
    opt => opt
        .UseMySql(connectionString, serverVersion)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableDetailedErrors()
        .EnableSensitiveDataLogging()
        .UseSnakeCaseNamingConvention()
);

var app = builder.Build();

// DbUp
new DbUpRunner(connectionString).Run();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();