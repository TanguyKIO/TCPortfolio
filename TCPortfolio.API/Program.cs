using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using CloudinaryDotNet;
using TCPortfolio.Infrastructure.Configurations;
using TCPortfolio.Infrastructure.Data;
using TCPortfolio.Application.Interfaces;
using TCPortfolio.Domain.Interfaces;
using TCPortfolio.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Get Cloudinary settings from appsettings.json
var cloudinarySettings = builder.Configuration
    .GetSection("CloudinarySettings")
    .Get<CloudinarySettings>();

if (cloudinarySettings == null)
{
    throw new InvalidOperationException("Cloudinary settings are not configured in appsettings.json");
}

// Create a Cloudinary account instance using the settings
var account = new Account(
    cloudinarySettings.CloudName,
    cloudinarySettings.ApiKey,
    cloudinarySettings.ApiSecret
);
// Inject the Cloudinary instance as a singleton service
builder.Services.AddSingleton(new Cloudinary(account));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("TCPortfolio.Infrastructure")));
builder.Services.AddAutoMapper(cfg => { }, typeof(PhotoMappingProfile));
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

