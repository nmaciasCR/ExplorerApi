var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<NASA_Asteroids.Services.NASA.Model.INasa, NASA_Asteroids.Services.NASA.Nasa>();
builder.Services.AddScoped<NASA_Asteroids.Business.Interfaces.IExplorer, NASA_Asteroids.Business.Explorer>();
builder.Services.AddScoped<NASA_Asteroids.Business.Interfaces.IAsteroids, NASA_Asteroids.Business.Asteroids>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
