using DocumentIngestion.Api.Infrastructure.DependencyInjection;
using DocumentIngestion.Api.Infrastructure.Extensions;
using DocumentIngestion.Api.Infrastructure.Middleware;
using DocumentIngestion.Api.Invoices.Context;
using DocumentIngestion.Api.Invoices.InvoiceEndpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwagger();
builder.Services.AddEndpointsApiExplorer();
// Add custom Swagger configuration
builder.Services.AddCustomSwagger();
// Add services to the container.
builder.Services.AddInvoiceServicesAndRepositories();

builder.Services.AddOpenApi();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddDbContext<InvoiceDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("InvoicesDb")));

var app = builder.Build();

app.ApplyMigrations();

app.MapInvoiceEndpoints();
app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapOpenApi("/openapi/v1.yaml");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();


app.UseHttpsRedirection();

app.Run();

public partial class Program { }// This partial class is necessary for the integration tests to work correctly.
