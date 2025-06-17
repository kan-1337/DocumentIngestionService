using DocumentIngestion.Api.Invoices.Models;

namespace DocumentIngestion.Api.Invoices.Repositories;
public interface IInvoiceRepository
{
    Task SaveAsync(Invoice invoice);
    Task<Invoice?> GetByIdAsync(Guid id);
    Task<bool> GetByInvoiceNumberAsync(string invoiceNumber, Guid supplierId);
    Task<Invoice> UpdateAsync(Invoice invoice);
    Task<List<Invoice>> GetAllAsync();
}
