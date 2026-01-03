using DotNetEnv;
using RestWithAspNet10Scaffold.Auth.Contract;
using RestWithAspNet10Scaffold.Auth.Tools;
using RestWithAspNet10Scaffold.Configurations;
using RestWithAspNet10Scaffold.Files.Exporters.Factory;
using RestWithAspNet10Scaffold.Files.Exporters.Impl;
using RestWithAspNet10Scaffold.Files.Importers.Factory;
using RestWithAspNet10Scaffold.Files.Importers.Impl;
using RestWithAspNet10Scaffold.Hypermedia.Filters;
using RestWithAspNet10Scaffold.Mail;
using RestWithAspNet10Scaffold.Repositories;
using RestWithAspNet10Scaffold.Repositories.Impl;
using RestWithAspNet10Scaffold.Services;
using RestWithAspNet10Scaffold.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

// Replace environment variables in configuration
var emailUsername = Environment.GetEnvironmentVariable("EMAIL_USERNAME");
var emailPassword = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");

if (!string.IsNullOrEmpty(emailUsername))
    builder.Configuration["Email:Username"] = emailUsername;
if (!string.IsNullOrEmpty(emailPassword))
    builder.Configuration["Email:Password"] = emailPassword;

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
builder.Services.AddEmailConfiguration(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDatabaseConfig(builder.Configuration);
builder.Services.AddEvolveConfig(builder.Configuration, builder.Environment);
builder.Services.AddAuthConfig(builder.Configuration);

builder.Services.AddScoped<IPersonServices, PersonService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IPasswordHasher, Sha256PasswordHasher>();
builder.Services.AddScoped<CsvFileImporter>();
builder.Services.AddScoped<ExcelFileImporter>();
builder.Services.AddScoped<FileImporterFactory>();
builder.Services.AddScoped<EmailSender>();

builder.Services.AddScoped<CsvFileExporter>();
builder.Services.AddScoped<ExcelFileExporter>();
builder.Services.AddScoped<FileExporterFactory>();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
// Necessário seguir está ordem.
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCorsConfig();

app.MapControllers();

app.UseSwaggerSpecification();
app.UseScalarSpecification();
app.UseHateoasRoutes();

app.Run();
