using DocumentIngestion.Api.Invoices.Dtos;
using DocumentIngestion.Api.Invoices.Models;
using DocumentIngestion.Api.Invoices.Services;
using Microsoft.AspNetCore.Authorization;
using Shared.Common.Extensions;
using Shared.Common.Models;

namespace DocumentIngestion.Api.Invoices.InvoiceEndpoints;
public static class InvoiceEndpoints
{
    public static void MapInvoiceEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/invoices")
            .WithTags("Invoices")
            .RequireAuthorization(); // All invoice endpoints require authentication by default

        // Create Invoice - Admin only
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
            .WithDescription("Creates a new draft invoice. Requires Admin role.")
            .RequireAuthorization("Admin")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError);

        // Get Invoice By Invoice Id - User or Admin
        group.MapGet("/{id:guid}", async (Guid id, IInvoiceService service) =>
        {
            var response = await service.GetByIdAsync(id);
            return Results.Ok(response);
        }).WithName("GetById")
            .WithSummary("Gets an invoice by invoice id")
            .WithDescription("Retrieves an invoice by its unique identifier. Requires User or Admin role.")
            .WithParameterDescriptions(("id", "The unique identifier of the invoice (Guid)"))
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound);

        // Export Invoice - Admin only
        group.MapPost("/{id}/export", async (Guid id, IInvoiceService service) =>
        {
            var response = await service.ExportInvoiceAsync(id);
            return Results.Ok(response);
        }).WithName("ExportInvoice")
            .WithSummary("Exports an invoice to external system")
            .WithDescription("Exports an invoice to external system. Requires Admin role.")
            .WithParameterDescriptions(("id", "The unique identifier of the invoice (Guid)"))
            .RequireAuthorization("Admin")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        // List Invoices with Pagination and Filtering - User or Admin
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
            .WithSummary("Lists all invoices with optional filtering and pagination")
            .WithDescription("Retrieves a paginated list of invoices with optional filters. Requires User or Admin role.")
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
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status500InternalServerError);

        // Delete Invoice - Admin only
        group.MapDelete("/{id:guid}", async (Guid id, IInvoiceService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        }).WithName("DeleteInvoice")
            .WithSummary("Deletes an invoice by its unique identifier")
            .WithDescription("Permanently removes an invoice from the system. Requires Admin role.")
            .WithParameterDescriptions(("id", "The unique identifier of the invoice to delete (Guid)"))
            .RequireAuthorization("Admin")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}
