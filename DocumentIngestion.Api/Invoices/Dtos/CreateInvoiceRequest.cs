using System.ComponentModel.DataAnnotations;

namespace DocumentIngestion.Api.Invoices.Dtos;
public class CreateInvoiceRequest
{
    /// <summary>
    ///  The Invoice Number Should be unique for each supplier
    /// </summary>
    public string InvoiceNumber { get; set; } = default!;

    /// <summary>
    ///  Unique identifier for Supplier
    /// </summary>

    public Guid SupplierId { get; set; }

    /// <summary>
    ///  Invoice Date, when invoice was issued
    /// </summary>

    public DateTime InvoiceDate { get; set; }

    /// <summary>
    /// Which currency the invoice is set to
    /// </summary>

    public string Currency { get; set; } = default!;

    /// <summary>
    /// Invoice Lines represents each cost and item listed
    /// </summary>

    public List<CreateInvoiceLineRequest> Lines { get; set; } = [];
}
