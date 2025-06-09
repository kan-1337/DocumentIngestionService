namespace DocumentIngestion.Api.Invoices.Dtos;
public record class InvoiceResponse
{
    public Guid Id { get; init; }
    public string InvoiceNumber { get; init; } = null!;
    public Guid SupplierId { get; init; }
    public DateTime InvoiceDate { get; init; }
    public decimal TotalAmount { get; init; }
    public string Currency { get; init; } = null!;
    public List<InvoiceLineResponse> Lines { get; init; } = [];
}
