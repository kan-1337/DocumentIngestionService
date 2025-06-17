using DocumentIngestion.Api.Invoices.Models;

namespace DocumentIngestion.Api.Invoices.Dtos;
public record class InvoiceResponse
{
    /// <summary>
    ///  Unique Identifier For the Invoice
    /// </summary>
    public Guid Id { get; init; }
    /// <summary>
    ///  The Invoice Number Should be unique for each supplier
    /// </summary>
    public string InvoiceNumber { get; init; } = null!;
    /// <summary>
    ///  Unique identifier for Supplier
    /// </summary>
    public Guid SupplierId { get; init; }
    /// <summary>
    ///  Invoice Date, when invoice was issued
    /// </summary>
    public DateTime InvoiceDate { get; init; }
    /// <summary>
    ///  The total amount of quantity * amount on invoice line
    /// </summary>
    public decimal TotalAmount { get; init; }
    /// <summary>
    /// Which currency the invoice is set to
    /// </summary>
    public string Currency { get; init; } = null!;
    /// <summary>
    ///  The status of the exportation of the invoice
    /// </summary>
    public InvoiceExportStatus InvoiceExportStatus { get; init; }
    /// <summary>
    /// Invoice Lines represents each cost and item listed
    /// </summary>
    public List<InvoiceLineResponse> Lines { get; init; } = [];
}
