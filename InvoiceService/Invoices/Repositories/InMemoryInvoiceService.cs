using DocumentIngestion.Api.Invoices.Models;
using Shared.Common.Exceptions;

namespace InvoiceService.Invoices.Repositories;
public class InMemoryInvoiceService : IInvoiceRepository
{
    private readonly Dictionary<Guid, Invoice> _store = new();

    public Task SaveAsync(Invoice invoice)
    {
        try
        {
            _store[invoice.Id] = invoice;
            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            throw new PersistenceException("Failed to save invoice.", ex);
        }
    }

    public Task<Invoice?> GetByIdAsync(Guid id)
    {
        _store.TryGetValue(id, out var invoice);
        return Task.FromResult(invoice);
    }
}
