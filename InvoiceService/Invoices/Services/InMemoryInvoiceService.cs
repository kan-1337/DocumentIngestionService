using InvoiceService.Invoices.Dtos;
using InvoiceService.Invoices.Models;

namespace InvoiceService.Invoices.Services;
public class InMemoryInvoiceService : IInvoiceService
{
    private readonly Dictionary<Guid, Invoice> _store = new();

    public Task<Guid> CreateInvoiceAsync(CreateInvoiceRequest request)
    {
        var invoice = new Invoice(
            request.InvoiceNumber,
            request.SupplierId,
            request.InvoiceDate
        );

        foreach (var line in request.Lines)
        {
            invoice.AddLine(line.Description, line.Quantity, line.UnitPrice);
        }

        _store[invoice.Id] = invoice;
        return Task.FromResult(invoice.Id);
    }

    public Task<Invoice?> GetByIdAsync(Guid id)
    {
        _store.TryGetValue(id, out var invoice);
        return Task.FromResult(invoice);
    }
}
