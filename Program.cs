using Microsoft.EntityFrameworkCore;
using minimal_api_shorterUrl.Data;

var builder = WebApplication.CreateBuilder(args);

// config ef.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapGet("/", () => "welcome to shorterUrl");

app.Run();