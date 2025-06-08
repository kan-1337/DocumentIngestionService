using InvoiceService.ExternalSystems;
using InvoiceService.Invoices.Dtos;
using InvoiceService.Invoices.Models;
using InvoiceService.Invoices.Repositories;
using Shared.Common.Exceptions;

namespace InvoiceService.Invoices.Services;
public class InvoiceProcessingService : IInvoiceService
{
    private readonly IInvoiceRepository _repo;
    private readonly IExternalSystemClient _external;
    private readonly ILogger<InvoiceProcessingService> _logger;

    public InvoiceProcessingService(IInvoiceRepository repo, IExternalSystemClient external, ILogger<InvoiceProcessingService> logger)
    {
        _repo = repo;
        _external = external;
        _logger = logger;
    }

    public async Task<Guid> CreateInvoiceAsync(CreateInvoiceRequest dto)
    {
        var invoice = new Invoice(dto.InvoiceNumber, dto.SupplierId, dto.InvoiceDate);

        foreach (var line in dto.Lines)
        {
            invoice.AddInvoiceLine(line.Description, line.Quantity, line.UnitPrice);
        }

        try
        {
            await _repo.SaveAsync(invoice);
            _logger.LogInformation("Invoice {InvoiceId} saved to repository.", invoice.Id);

            var notifySupplierTask = _external.NotifySupplierAsync(invoice.SupplierId);
            var notifyPaymentTask = _external.NotifyPaymentServiceAsync(invoice.Id, invoice.TotalAmount);

            await Task.WhenAll(notifySupplierTask, notifyPaymentTask);

            _logger.LogInformation("External systems notified for invoice {InvoiceId}", invoice.Id);
        }
        catch (PersistenceException ex)
        {
            _logger.LogError(ex, "Persistence failure");
            throw;
        }

        return invoice.Id;
    }

    public async Task<Invoice> GetByIdAsync(Guid id)
    {
        var invoice = await _repo.GetByIdAsync(id);

        if (invoice is null)
        { 
            throw new NotFoundException<Invoice, Guid>(id); 
        }

        return invoice;
    }
}
