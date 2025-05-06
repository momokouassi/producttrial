using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProductTrial.Api.Convertors;
using ProductTrial.Data.Entities;
using ProductTrial.Services.Interfaces;
using ProductTrial.Services.Middlewares.ExceptionHandler;
using ProductTrial.Services.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString(ProductTrialDbContext.ProductTrialDbConnection);
builder.Services.AddDbContextFactory<ProductTrialDbContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 4, 32))));

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.Converters.Add(new AllCapitalizeEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();

app.Run();