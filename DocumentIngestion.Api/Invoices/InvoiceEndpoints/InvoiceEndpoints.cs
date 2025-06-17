using DocumentIngestion.Api.Invoices.Dtos;
using DocumentIngestion.Api.Invoices.Maping;
using DocumentIngestion.Api.Invoices.Models;
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
            .WithSummary("Creates a new Invoice")
            .WithDescription("Creates a new draft response.")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);


        group.MapGet("/{id:guid}", async (Guid id, IInvoiceService service) =>
        {
            var response = await service.GetByIdAsync(id);
            return Results.Ok(response);
        }).WithName("GetById")
            .WithSummary("Gets an response by response id.")
            .WithOpenApi(operation =>
            {
                var idParam = operation.Parameters.FirstOrDefault(p => p.Name == "id");
                if (idParam != null)
                {
                    idParam.Description = "Unique identifier for the invoice (Guid)";
                }
                return operation;
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/invoices/{id}/export", async (Guid id, IInvoiceService service) =>
        {
            var response = await service.ExportInvoiceAsync(id);
            return Results.Ok(response);
        }).WithName("ExportInvoice")
            .WithSummary("Exports an invoice to external system")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}
