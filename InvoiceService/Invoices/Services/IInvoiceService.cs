using InvoiceService.Invoices.Dtos;
using InvoiceService.Invoices.Models;

namespace InvoiceService.Invoices.Services;
public interface IInvoiceService
{
    /// <summary>
    ///  Creates a new invoice object 
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Invoice ID as <see cref="Guid"/></returns>
    Task<Guid> CreateInvoiceAsync(CreateInvoiceRequest request);

    /// <summary>
    ///  Gets an invoice by invoice id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Returns an invoice object <see cref="Invoice"/></returns>
    Task<Invoice> GetByIdAsync(Guid id);
}
