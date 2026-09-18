using Microsoft.EntityFrameworkCore;
using minimal_api_shorterUrl.Data;
using minimal_api_shorterUrl.Services;
using minimal_api_shorterUrl.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// config ef.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// services
builder.Services.AddScoped<UrlShortenerService>();

// config swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ui swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "welcome to shorterUrl");

// endpoints
app.MapUrlEndpoints();

app.Run();