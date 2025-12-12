using DocumentIngestion.Api.Invoices.Models;

namespace DocumentIngestion.Api.ExternalSystems;
public interface IExternalSystemClient
{
    Task NotifySupplierAsync(Guid supplierId);
    Task NotifyCustomerAsync(Guid customerId);
    Task NotifyPaymentServiceAsync(Guid invoiceId, decimal amount);
    Task ExportAsync(Invoice invoice);
}
