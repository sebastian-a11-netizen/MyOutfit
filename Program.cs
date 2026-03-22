using Infraestructure;
using Domain;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var path = Path.Combine(Directory.GetCurrentDirectory(), "MyOutfit.db");

builder.Services.AddScoped<IUserRepository>(provider => new UserRepository($"Data Source={path}"));
builder.Services.AddScoped<IClothingRepository>(provider => new ClothingRepository($"Data Source={path}"));
builder.Services.AddScoped<IClothingService, ClothingService>();

var app = builder.Build();

app.UseStaticFiles();

app.MapControllers();

app.Run();
