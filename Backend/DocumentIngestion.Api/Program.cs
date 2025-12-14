using DocumentIngestion.Api.Auth.AuthEndpoints;
using DocumentIngestion.Api.Infrastructure.DependencyInjection;
using DocumentIngestion.Api.Infrastructure.Extensions;
using DocumentIngestion.Api.Infrastructure.Middleware;
using DocumentIngestion.Api.Invoices.Context;
using DocumentIngestion.Api.Invoices.InvoiceEndpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwagger();
builder.Services.AddInvoiceServicesAndRepositories();

builder.Services.AddAuthServices(builder.Configuration);

builder.Services.AddHealthChecks();

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

// Apply database migrations
app.ApplyMigrations();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/health").AllowAnonymous();

// Register endpoints
app.MapAuthEndpoints();
app.MapInvoiceEndpoints();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.Run();

public partial class Program { }// This partial class is necessary for the integration tests to work correctly.
