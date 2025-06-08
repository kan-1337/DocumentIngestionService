namespace InvoiceService.Invoices.Dtos;
public class InvoiceDto
{
    public Guid Id { get; set; }
    public string Number { get; set; } = default!;
}
