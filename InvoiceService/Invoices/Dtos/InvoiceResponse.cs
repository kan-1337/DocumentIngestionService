namespace InvoiceService.Invoices.Dtos;
public class InvoiceResponse
{
    public Guid Id { get; set; }
    public string InvoiceNumber { get; set; } = null!;
    public Guid SupplierId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Currency { get; set; } = null!;
    public List<InvoiceLineResponse> Lines { get; set; } = new();
}
