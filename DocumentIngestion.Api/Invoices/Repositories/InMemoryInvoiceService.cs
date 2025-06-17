using DocumentIngestion.Api.Invoices.Models;
using Shared.Common.Exceptions;

namespace DocumentIngestion.Api.Invoices.Repositories;
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

    public Task<List<Invoice>> GetAllAsync()
    {
        return Task.FromResult(_store.Values.ToList());
    }

    public Task<bool> GetByInvoiceNumberAsync(string invoiceNumber, Guid supplierId)
    {
        var invoiceExists = _store.Values.Any(x => x.SupplierId == supplierId && x.InvoiceNumber == invoiceNumber);
        return Task.FromResult(invoiceExists);
    }

    public Task<Invoice> UpdateAsync(Invoice invoice)
    {
        if (!_store.ContainsKey(invoice.Id))
        {
            throw new NotFoundException<Invoice, Guid>(invoice.Id);
        }

        _store[invoice.Id] = invoice;
        return Task.FromResult(_store[invoice.Id]);
    }
}
