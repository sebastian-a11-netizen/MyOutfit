using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped(_ => new UserService("Data Source=data/MyOutfit.db"));

var app = builder.Build();

app.UseStaticFiles();

app.MapControllers();

app.Run();
