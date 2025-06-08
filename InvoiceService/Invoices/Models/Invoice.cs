using Shared.Common.Exceptions;
using Shared.Common.Models;

namespace DocumentIngestion.Api.Invoices.Models;
public class Invoice : EntityBase
{
    public Guid SupplierId { get; set; }
    public string InvoiceNumber { get; private set; } = null!;
    public DateTime InvoiceDate { get; private set; }
    public InvoiceStatus Status { get; private set; } = InvoiceStatus.Draft;

    private readonly List<InvoiceLine> _lines = new();
    public IReadOnlyCollection<InvoiceLine> Lines => _lines.AsReadOnly();

    public decimal TotalAmount => _lines.Sum(l => l.Total);

    public Invoice(string invoiceNumber, Guid supplierId, DateTime invoiceDate)
    {
        if (string.IsNullOrWhiteSpace(invoiceNumber))
        {
            throw new BadRequestException("Invoice number is required");
        }

        InvoiceNumber = invoiceNumber;
        SupplierId = supplierId;
        InvoiceDate = invoiceDate;
    }

    public void AddInvoiceLine(string description, int quantity, decimal unitPrice)
    {
        if (Status is not InvoiceStatus.Draft)
        {
            throw new DomainValidationException("Can only add lines to a draft invoice");
        }

        _lines.Add(new InvoiceLine(description, quantity, unitPrice));
    }

    public void BookInvoice()
    {
        if (_lines.Count == 0)
        {
            throw new DomainValidationException("Cannot submit an invoice without line items");
        }

        Status = InvoiceStatus.Booked;
    }

    public void MarkAsPaid()
    {
        if (Status is not InvoiceStatus.Booked)
        {
            throw new DomainValidationException("Only submitted invoices can be marked as paid");
        }

        Status = InvoiceStatus.Paid;
    }
}
