using DocumentIngestion.Api.Invoices.Models;

namespace DocumentIngestion.Api.Invoices.Repositories;
public interface IInvoiceRepository
{
    Task SaveAsync(Invoice invoice);
    Task<Invoice?> GetByIdAsync(Guid id);
}
