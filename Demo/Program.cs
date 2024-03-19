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

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

