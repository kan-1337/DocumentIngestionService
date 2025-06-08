using System.ComponentModel.DataAnnotations;

namespace InvoiceService.Invoices.Dtos;
public class InvoiceLineRequest
{
    [Required] 
    public string Description { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
