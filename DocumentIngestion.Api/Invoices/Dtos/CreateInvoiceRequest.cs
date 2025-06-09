using System.ComponentModel.DataAnnotations;

namespace DocumentIngestion.Api.Invoices.Dtos;
public class CreateInvoiceRequest
{
    [Required]
    public string InvoiceNumber { get; set; } = default!;

    public Guid SupplierId { get; set; }

    [Required]
    public DateTime InvoiceDate { get; set; }

    [Required]
    public string Currency { get; set; } = default!;

    [Required]
    [MinLength(1, ErrorMessage = "At least one line item required.")]
    public List<InvoiceLineRequest> Lines { get; set; } = [];
}
