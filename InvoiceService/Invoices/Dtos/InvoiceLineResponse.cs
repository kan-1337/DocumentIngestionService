namespace InvoiceService.Invoices.Dtos;
public class InvoiceLineResponse
{
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total { get; set; }
}
