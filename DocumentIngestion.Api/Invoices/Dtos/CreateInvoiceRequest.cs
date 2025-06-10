using System.ComponentModel.DataAnnotations;

namespace DocumentIngestion.Api.Invoices.Dtos;
public class CreateInvoiceRequest
{
    public string InvoiceNumber { get; set; } = default!;

    public Guid SupplierId { get; set; }

    public DateTime InvoiceDate { get; set; }

    public string Currency { get; set; } = default!;

    public List<CreateInvoiceLineRequest> Lines { get; set; } = [];
}
