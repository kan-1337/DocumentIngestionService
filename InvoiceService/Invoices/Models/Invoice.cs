using Shared.Common.Models;

namespace InvoiceService.Invoices.Models;
public class Invoice : EntityBase
{
    public string InvoiceNumber { get; private set; }
    public DateTime InvoiceDate { get; private set; }
    public InvoiceStatus Status { get; private set; } = InvoiceStatus.Draft;

    private readonly List<InvoiceLine> _lines = new();
    public IReadOnlyCollection<InvoiceLine> Lines => _lines.AsReadOnly();

    public decimal TotalAmount => _lines.Sum(l => l.Total);
}
