using DocumentIngestion.Api.Invoices.Dtos;
using DocumentIngestion.Api.Invoices.Maping;
using DocumentIngestion.Api.Invoices.Services;

namespace DocumentIngestion.Api.Invoices.InvoiceEndpoints;
public static class InvoiceEndpoints
{
    public static void MapInvoiceEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/invoices").WithTags("Invoices");

        
        group.MapPost("/", async (CreateInvoiceRequest dto, IInvoiceService service) =>
        {
            if (dto.Lines is null || dto.Lines.Count == 0)
            {
                return Results.BadRequest(new { message = "At least one line item is required." });
            }

            var invoiceId = await service.CreateInvoiceAsync(dto);
            return Results.Created($"/invoices/{invoiceId}", new { invoiceId });
        }).WithName("CreateInvoice")
            .WithSummary("Creates a new invoice")
            .WithDescription("Creates a new draft invoice.")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);


        group.MapGet("/{id:guid}", async (Guid id, IInvoiceService service) =>
        {
            var invoice = await service.GetByIdAsync(id);
            var response = invoice.ToResponse();
            return Results.Ok(response);
        }).WithName("GetById")
            .WithSummary("Gets an invoice by invoice id.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }
}
