using Infraestructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var path = Path.Combine(Directory.GetCurrentDirectory(), "MyOutfit.db");

builder.Services.AddScoped<IUserRepository>(provider => new UserRepository($"Data Source={path}"));

var app = builder.Build();

app.UseStaticFiles();

app.MapControllers();

app.Run();
