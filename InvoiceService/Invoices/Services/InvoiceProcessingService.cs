using InvoiceService.Invoices.Dtos;
using InvoiceService.Invoices.Models;
using InvoiceService.Invoices.Repositories;
using Shared.Common.Exceptions;

namespace InvoiceService.Invoices.Services;
public class InvoiceProcessingService : IInvoiceService
{
    private readonly IInvoiceRepository _repo;
    public InvoiceProcessingService(IInvoiceRepository repo) => _repo = repo;

    public async Task<Guid> CreateInvoiceAsync(CreateInvoiceRequest dto)
    {
        var invoice = new Invoice(dto.InvoiceNumber, dto.SupplierId, dto.InvoiceDate);

        foreach (var line in dto.Lines)
        {
            invoice.AddInvoiceLine(line.Description, line.Quantity, line.UnitPrice);
        }

        await _repo.SaveAsync(invoice);
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
