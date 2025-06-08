using InvoiceService.ExternalSystems;
using InvoiceService.Infrastructure.Middleware;
using InvoiceService.Invoices.InvoiceEndpoints;
using InvoiceService.Invoices.Repositories;
using InvoiceService.Invoices.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IInvoiceRepository, InMemoryInvoiceService>();
builder.Services.AddScoped<IInvoiceService, InvoiceProcessingService>();
builder.Services.AddSingleton<IExternalSystemClient, FakeExternalSystemClient>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
var app = builder.Build();

app.MapInvoiceEndpoints();
app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.Run();
