using System.Reflection;
using Demo.Data;
using Microsoft.EntityFrameworkCore;
using Demo.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FeatureFlagDbContext>(options =>
{
    options.UseInMemoryDatabase("FeatureFlagDb");
});

// Add services to the container.
builder.Services.AddScoped<IFeatureFlagService, FeatureFlagService>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

