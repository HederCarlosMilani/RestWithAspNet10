using RestWithAspNet10Scaffold.Configurations;
using RestWithAspNet10Scaffold.Repositories;
using RestWithAspNet10Scaffold.Repositories.Impl;
using RestWithAspNet10Scaffold.Services;
using RestWithAspNet10Scaffold.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddSeriLogLogging();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDatabaseConfig(builder.Configuration);
builder.Services.AddEvolveConfig(builder.Configuration, builder.Environment);

builder.Services.AddScoped<IPersonServices, PersonService>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
