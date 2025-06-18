using DocumentIngestion.Api.Invoices.Dtos;
using DocumentIngestion.Api.Invoices.Models;
using DocumentIngestion.Api.Invoices.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Extensions;
using Shared.Common.Models;

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


        group.MapGet("/{id:guid}", async ([FromQuery] Guid id, IInvoiceService service) =>
        {
            var response = await service.GetByIdAsync(id);
            return Results.Ok(response);
        }).WithName("GetById")
            .WithSummary("Gets an response by response id.")
            .WithParameterDescriptions(("id", "The unique identifier of the invoice (Guid)"))
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/invoices/{id}/export", async ([FromQuery] Guid id, IInvoiceService service) =>
        {
            var response = await service.ExportInvoiceAsync(id);
            return Results.Ok(response);
        }).WithName("ExportInvoice")
            .WithSummary("Exports an invoice to external system")
            .WithParameterDescriptions(("id", "The unique identifier of the invoice (Guid)"))
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("/", async ([AsParameters]  InvoiceQueryFilter filter, IInvoiceService service) =>
        {
            var result = await service.GetPagedAsync(
                            page: filter.Page, 
                            pageSize: filter.PageSize, 
                            supplierId: filter.SupplierId, 
                            status: filter.Status, 
                            from: filter.From, 
                            to: filter.To);

            return Results.Ok(result);
        }).WithName("ListInvoices")
            .WithSummary("Lists all invoices with optional filtering and pagination.")
            .WithParameterDescriptions(
                ("Page", "The page number to retrieve. Must be greater than 0."),
                ("PageSize", "The number of invoices per page. Must be greater than 0."),
                ("SupplierId", "Optional. Filter by the supplier's unique identifier."),
                ("Status", "Optional. Filter by invoice export status: NotExported (0), Exporting (1), Exported (2), ExportFailed (3)."),
                ("From", "Optional. Filter invoices with an issue date from this date (inclusive)."),
                ("To", "Optional. Filter invoices with an issue date up to this date (inclusive).")
            )
            .Produces<PagedResult<InvoiceResponse>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}
