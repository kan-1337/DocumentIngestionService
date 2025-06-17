using DocumentIngestion.Api.Invoices.Dtos;
using DocumentIngestion.Api.Invoices.Models;
using Shared.Common.Exceptions;
using Shared.Common.Models;

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

    /// <summary>
    /// Retrieves a paginated and filterable list of invoices.
    /// </summary>
    /// <param name="page">The page number</param>
    /// <param name="pageSize">The number of invoices per page</param>
    /// <param name="supplierId">The supplier's unique id</param>
    /// <param name="status">Filter by invoice status (e.g., "Draft", "Exported").</param>
    /// <param name="from">Filter invoices with an issue date from this date (inclusive).</param>
    /// <param name="to">Filter invoices with an issue date up to this date (inclusive).</param>
    /// <returns>
    /// Returns a <see cref="PagedResult{InvoiceResponse}"/> containing a list of invoices and paging metadata.
    /// </returns>
    /// <exception cref="DomainValidationException">
    /// Thrown if <paramref name="page"/> or <paramref name="pageSize"/> are less than 1.
    /// </exception>
    /// <exception cref="Exception">
    /// Thrown if an unexpected error occurs during the retrieval.
    /// </exception>
    Task<PagedResult<InvoiceResponse>> GetPagedAsync(
        int page,
        int pageSize,
        Guid? supplierId,
        InvoiceExportStatus? status,
        DateTime? from,
        DateTime? to);
}
