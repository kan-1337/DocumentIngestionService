namespace InvoiceService.Invoices.Dtos;
public class InvoiceLineDto
{
    public string Description { get; set; } = default!;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }
}
