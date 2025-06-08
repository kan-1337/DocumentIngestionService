using System.ComponentModel.DataAnnotations;
using InvoiceService.Invoices.Models;

namespace InvoiceService.Invoices.Dtos;
public class CreateInvoiceRequest
{
    [Required] public string InvoiceNumber { get; set; } = default!;
    [Required] public Guid SupplierId { get; set; }
    [Required] public DateTime InvoiceDate { get; set; }
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Total must be > 0")]
    public decimal TotalAmount { get; set; }
    [Required] public string Currency { get; set; } = default!;
    [Required]
    [MinLength(1, ErrorMessage = "At least one line item required.")]
    public List<InvoiceLineRequest> Lines { get; set; } = new();
}
