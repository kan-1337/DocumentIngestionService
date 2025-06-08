namespace InvoiceService.Invoices.Models;
public class InvoiceLine
{
    public string Description { get; }
    public int Quantity { get; }
    public decimal UnitPrice { get; }
    public decimal Total => Quantity * UnitPrice;
}
