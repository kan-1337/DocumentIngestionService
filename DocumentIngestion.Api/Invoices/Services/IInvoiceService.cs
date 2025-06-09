using DocumentIngestion.Api.Invoices.Dtos;
using DocumentIngestion.Api.Invoices.Models;

namespace DocumentIngestion.Api.Invoices.Services;
public interface IInvoiceService
{
    /// <summary>
    ///  Creates a new invoice object as draft and saves it to the repository.
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Invoice ID as <see cref="Guid"/></returns>
    Task<Guid> CreateInvoiceAsync(CreateInvoiceRequest request);

    /// <summary>
    ///  Gets an invoice by invoice id, which is a <see cref="Guid"/>.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Returns an invoice object <see cref="Invoice"/></returns>
    Task<Invoice> GetByIdAsync(Guid id);
}
