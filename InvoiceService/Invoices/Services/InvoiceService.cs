using InvoiceService.Invoices.Dtos;
using InvoiceService.Invoices.Models;
using InvoiceService.Invoices.Repositories;

namespace InvoiceService.Invoices.Services;
public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _repo;
    public InvoiceService(IInvoiceRepository repo) => _repo = repo;

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

    public Task<Invoice?> GetByIdAsync(Guid id) => _repo.GetByIdAsync(id);
}
