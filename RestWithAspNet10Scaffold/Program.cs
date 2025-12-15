using RestWithAspNet10Scaffold.Configurations;
using RestWithAspNet10Scaffold.Service;
using RestWithAspNet10Scaffold.Service.Impl;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddSeriLogLogging();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDatabaseConfig(builder.Configuration);

builder.Services.AddScoped<IPersonServices, PersonServiceImpl>();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
