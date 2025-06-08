namespace DocumentIngestion.Api.Invoices.Dtos;
public record class InvoiceLineResponse
{
    public string? Description { get; init; }
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal Total { get; init; }
}
