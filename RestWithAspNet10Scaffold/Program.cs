using DotNetEnv;
using RestWithAspNet10Scaffold.Configurations;
using RestWithAspNet10Scaffold.Files.Exporters.Factory;
using RestWithAspNet10Scaffold.Files.Exporters.Impl;
using RestWithAspNet10Scaffold.Files.Importers.Factory;
using RestWithAspNet10Scaffold.Files.Importers.Impl;
using RestWithAspNet10Scaffold.Hypermedia.Filters;
using RestWithAspNet10Scaffold.Repositories;
using RestWithAspNet10Scaffold.Repositories.Impl;
using RestWithAspNet10Scaffold.Services;
using RestWithAspNet10Scaffold.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.AddServiceDefaults();
builder.AddSeriLogLogging();

// Add services to the container.

builder.Services.AddControllers(options =>
    {
        options.Filters.Add<HypermediaFilter>();
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiConfig();
builder.Services.AddSwaggerConfig();
builder.Services.AddRoutesConfig();
builder.Services.AddCorsConfig(builder.Configuration);
builder.Services.AddHateoasConfiguration();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDatabaseConfig(builder.Configuration);
builder.Services.AddEvolveConfig(builder.Configuration, builder.Environment);

builder.Services.AddScoped<IPersonServices, PersonService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

builder.Services.AddScoped<CsvFileImporter>();
builder.Services.AddScoped<ExcelFileImporter>();
builder.Services.AddScoped<FileImporterFactory>();

builder.Services.AddScoped<CsvFileExporter>();
builder.Services.AddScoped<ExcelFileExporter>();
builder.Services.AddScoped<FileExporterFactory>();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.

app.UseAuthorization();
app.UseRouting();
app.UseCorsConfig();

app.MapControllers();

app.UseSwaggerSpecification();
app.UseScalarSpecification();
app.UseHateoasRoutes();

app.Run();
