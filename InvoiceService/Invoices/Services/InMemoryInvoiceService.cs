using InvoiceService.Invoices.Models;
using InvoiceService.Invoices.Repositories;

namespace InvoiceService.Invoices.Services;
public class InMemoryInvoiceService : IInvoiceRepository
{
    private readonly Dictionary<Guid, Invoice> _store = new();

    public Task SaveAsync(Invoice invoice)
    {
        _store[invoice.Id] = invoice;
        return Task.CompletedTask;
    }

    public Task<Invoice?> GetByIdAsync(Guid id)
    {
        _store.TryGetValue(id, out var invoice);
        return Task.FromResult(invoice);
    }
}
