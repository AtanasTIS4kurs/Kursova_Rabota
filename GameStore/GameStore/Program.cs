using FluentValidation;
using FluentValidation.AspNetCore;
using GameStore.DL;
using GameStore.Models.Configuration;
using GameStore.Models.Requests;
using GameStore.Validator;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithThreadId()
    .Enrich.WithProcessId()
    .Enrich.WithMachineName()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
// Add services to the container.
builder.Services.Configure<MongoDbConfiguration>(
               builder.Configuration.GetSection(nameof(MongoDbConfiguration)));
builder.Services.RegisterRepositories();
builder.Services.RegisterServices();
builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IValidator<AddGameRequest>, RequestValidator>();
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();
app.MapHealthChecks("/healthz");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
