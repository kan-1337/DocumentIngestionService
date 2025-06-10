using DocumentIngestion.Api.ExternalSystems;
using DocumentIngestion.Api.Infrastructure.Middleware;
using DocumentIngestion.Api.Invoices.InvoiceEndpoints;
using DocumentIngestion.Api.Invoices.Repositories;
using DocumentIngestion.Api.Invoices.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IInvoiceRepository, InMemoryInvoiceService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddSingleton<IExternalSystemClient, FakeExternalSystemClient>();
builder.Services.AddOpenApi();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
var app = builder.Build();

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

app.UseHttpsRedirection();



app.Run();

public partial class Program { }// This partial class is necessary for the integration tests to work correctly.
