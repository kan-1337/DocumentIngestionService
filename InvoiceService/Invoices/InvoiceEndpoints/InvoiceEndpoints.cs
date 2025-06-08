using InvoiceService.Invoices.Dtos;

namespace InvoiceService.Invoices.InvoiceEndpoints;
public static class InvoiceEndpoints
{
    public static void MapInvoiceEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/invoices").WithTags("Invoices");

        group.MapPost("/", (InvoiceDto dto) =>
        {
            if (string.IsNullOrWhiteSpace(dto.Number))
            {
                return Results.BadRequest(new { error = "Invoice number is required" });
            }

            dto.Id = Guid.NewGuid();
            return Results.Ok(dto);
        });

        group.MapGet("/{id:guid}", (Guid id) =>
        {
            return Results.Ok(new InvoiceDto
            {
                Id = id,
                Number = "TEST-123"
            });
        });
    }
}
