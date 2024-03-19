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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

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

