namespace DocumentIngestion.Api.Invoices.Dtos;
public class InvoiceLineDto
{
    public string? Description { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }
}
