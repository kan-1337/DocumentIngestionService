using InvoiceService.Invoices.InvoiceEndpoints;
using InvoiceService.Invoices.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IInvoiceService, InMemoryInvoiceService>();

var app = builder.Build();
app.MapInvoiceEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.Run();
