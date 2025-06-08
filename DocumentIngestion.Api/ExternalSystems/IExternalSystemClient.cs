namespace DocumentIngestion.Api.ExternalSystems;
public interface IExternalSystemClient
{
    Task NotifySupplierAsync(Guid supplierId);
    Task NotifyCustomerAsync(Guid customerId);
    Task NotifyPaymentServiceAsync(Guid invoiceId, decimal amount);
}
