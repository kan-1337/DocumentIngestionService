using DocumentIngestion.Api.Invoices.Context;
using DocumentIngestion.Api.Invoices.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Exceptions;

namespace DocumentIngestion.Api.Invoices.Repositories;

public class EfInvoiceRepository : IInvoiceRepository
{
    private readonly InvoiceDbContext _db;

    public EfInvoiceRepository(InvoiceDbContext db)
    {
        _db = db;
    }

    public async Task SaveAsync(Invoice invoice)
    {
        try
        {
            _db.Invoices.Add(invoice);
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new PersistenceException("Failed to save invoice.", ex);
        }
    }

    public Task<Invoice?> GetByIdAsync(Guid id)
        => _db.Invoices.FirstOrDefaultAsync(x => x.Id == id);

    public Task<List<Invoice>> GetAllAsync()
        => _db.Invoices.ToListAsync();

    public Task<bool> GetByInvoiceNumberAsync(string invoiceNumber, Guid supplierId)
        => _db.Invoices.AnyAsync(x => x.SupplierId == supplierId && x.InvoiceNumber == invoiceNumber);

    public async Task<Invoice> UpdateAsync(Invoice invoice)
    {
        var exists = await _db.Invoices.AnyAsync(x => x.Id == invoice.Id);
        if (!exists)
        {
            throw new NotFoundException<Invoice, Guid>(invoice.Id);
        }

        _db.Invoices.Update(invoice);
        await _db.SaveChangesAsync();
        return invoice;
    }

    public async Task DeleteAsync(Guid id)
    {
        var invoice = await _db.Invoices.FirstOrDefaultAsync(x => x.Id == id);

        if (invoice is null)
        {
            throw new NotFoundException<Invoice, Guid>(id);
        }

        try
        {
            _db.Invoices.Remove(invoice);
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new PersistenceException("Failed to delete invoice.", ex);
        }
    }
}
