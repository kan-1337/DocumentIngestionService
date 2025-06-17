using Shared.Common.Exceptions;
using Shared.Common.Models;

namespace DocumentIngestion.Api.Invoices.Models;
public class Invoice : EntityBase
{
    public Guid SupplierId { get; set; }
    public string InvoiceNumber { get; private set; }
    public DateTime InvoiceDate { get; private set; }
    public InvoiceStatus Status { get; private set; } = InvoiceStatus.Draft;
    public InvoiceExportStatus InvoiceExportStatus { get; set; } = InvoiceExportStatus.NotExported;
    public DateTime? ExportedAt { get; set; }
    public string? ExportErrorMessage { get; set; }
    public string Currency { get; private set; } = default!;

    private readonly List<InvoiceLine> _lines = [];
    public IReadOnlyCollection<InvoiceLine> Lines => _lines.AsReadOnly();

    public decimal TotalAmount => _lines.Sum(l => l.Total);

    public Invoice(string invoiceNumber, Guid supplierId, DateTime invoiceDate, string? currency)
    {
        if (string.IsNullOrWhiteSpace(invoiceNumber))
        {
            throw new BadRequestException("Invoice number is required.");
        }

        // Validate that the invoice number is unique in the context of your application,
        // data annotation aren't working with minimal apis yet, so we do it manually here
        // Could be custom attribute or a service that checks the uniqueness whenever minal apis get updated
        if (supplierId == Guid.Empty)
        {
            throw new BadRequestException("The Supplier ID is required for processing.");
        }

        InvoiceNumber = invoiceNumber;
        SupplierId = supplierId;
        InvoiceDate = invoiceDate == default ? DateTime.UtcNow : invoiceDate; // Or make it optional and throw if null
        Currency = !string.IsNullOrWhiteSpace(currency) ? currency : ""; // Real world solutions might default to whatever currency customer has or product, etc 
    }

    public void AddInvoiceLine(string? description, int quantity, decimal unitPrice)
    {
        if (Status is not InvoiceStatus.Draft)
        {
            throw new DomainValidationException("Can only add lines to a draft invoice");
        }

        _lines.Add(new InvoiceLine(description, quantity, unitPrice));
    }
}
