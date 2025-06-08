using DocumentIngestion.Api.Invoices.Models;

namespace InvoiceService.Invoices.Repositories;
public interface IInvoiceRepository
{
    Task SaveAsync(Invoice invoice);
    Task<Invoice?> GetByIdAsync(Guid id);
}
