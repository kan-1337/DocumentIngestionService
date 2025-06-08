using InvoiceService.Invoices.Dtos;
using InvoiceService.Invoices.Maping;
using InvoiceService.Invoices.Services;

namespace InvoiceService.Invoices.InvoiceEndpoints;
public static class InvoiceEndpoints
{
    public static void MapInvoiceEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/invoices").WithTags("Invoices");

        
        group.MapPost("/", async (CreateInvoiceRequest dto, IInvoiceService service) =>
        {
            if (dto.Lines == null || dto.Lines.Count == 0)
            {
                return Results.BadRequest(new { message = "At least one line item is required." });
            }

            var invoiceId = await service.CreateInvoiceAsync(dto);
            return Results.Created($"/invoices/{invoiceId}", new { invoiceId });
        });


        group.MapGet("/{id:guid}", async (Guid id, IInvoiceService service) =>
        {
            var invoice = await service.GetByIdAsync(id);

            if (invoice is null)
            {
                return Results.NotFound(new { message = $"Invoice with ID '{id}' was not found." });
            }

            var response = invoice.ToResponse();
            return Results.Ok(response);
        });
    }
}
