namespace InvoiceService.ExternalSystems;
public class FakeExternalSystemClient : IExternalSystemClient
{
    private readonly ILogger<FakeExternalSystemClient> _logger;

    public FakeExternalSystemClient(ILogger<FakeExternalSystemClient> logger)
    {
        _logger = logger;
    }

    public async Task NotifySupplierAsync(Guid supplierId)
    {
        _logger.LogInformation("Notifying supplier {SupplierId}...", supplierId);
        await Task.Delay(500);
    }

    public async Task NotifyCustomerAsync(Guid customerId)
    {
        _logger.LogInformation("Notifying customer {CustomerId}...", customerId);
        await Task.Delay(500);
    }

    public async Task NotifyPaymentServiceAsync(Guid invoiceId, decimal amount)
    {
        _logger.LogInformation("Notifying payment service for invoice {InvoiceId} with amount {Amount}...", invoiceId, amount);
        await Task.Delay(500);
    }
}
