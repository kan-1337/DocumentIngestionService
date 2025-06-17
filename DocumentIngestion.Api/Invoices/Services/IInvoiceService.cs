using DocumentIngestion.Api.Invoices.Dtos;
using DocumentIngestion.Api.Invoices.Models;

namespace DocumentIngestion.Api.Invoices.Services;
public interface IInvoiceService
{
    /// <summary>
    ///  Creates a new invoice object as draft and saves it to the repository.
    /// </summary>
    /// <param name="request"><see cref="CreateInvoiceRequest"/> request model used to create new draft invoice</param>
    /// <returns>Invoice ID as <see cref="Guid"/></returns>
    Task<Guid> CreateInvoiceAsync(CreateInvoiceRequest request);

    /// <summary>
    ///  Gets an invoice by invoice id, which is a <see cref="Guid"/>.
    /// </summary>
    /// <param name="id">Invoice Id</param>
    /// <returns>Returns an invoice object <see cref="InvoiceResponse"/></returns>
    Task<InvoiceResponse> GetByIdAsync(Guid id);

    /// <summary>
    /// Exports and invoice and updates the export status.
    /// </summary>
    /// <param name="id">Invoice Id</param>
    /// <returns>A invoice response model <see cref="InvoiceResponse"/></returns>
    Task<InvoiceResponse> ExportInvoiceAsync(Guid id);
}
