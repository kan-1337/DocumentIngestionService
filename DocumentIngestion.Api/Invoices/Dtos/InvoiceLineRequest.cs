namespace DocumentIngestion.Api.Invoices.Dtos;
public class InvoiceLineRequest
{
    public string Description { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
