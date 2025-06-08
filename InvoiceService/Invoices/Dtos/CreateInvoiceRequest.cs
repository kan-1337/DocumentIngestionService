using System.ComponentModel.DataAnnotations;
using InvoiceService.Invoices.Models;

namespace InvoiceService.Invoices.Dtos;
public class CreateInvoiceRequest
{
    public string InvoiceNumber { get; set; } = default!;
    public Guid SupplierId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Currency { get; set; } = default!;
    public List<InvoiceLineDto> Lines { get; set; } = new();
}
