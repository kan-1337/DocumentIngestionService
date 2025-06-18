using DocumentIngestion.Api.ExternalSystems;
using DocumentIngestion.Api.Invoices.Dtos;
using DocumentIngestion.Api.Invoices.Maping;
using DocumentIngestion.Api.Invoices.Models;
using DocumentIngestion.Api.Invoices.Repositories;
using Shared.Common.Exceptions;
using Shared.Common.Models;

namespace DocumentIngestion.Api.Invoices.Services;
public class InvoiceService  : IInvoiceService
{
    private readonly IInvoiceRepository _repo;
    private readonly IExternalSystemClient _external;
    private readonly ILogger<InvoiceService > _logger;

    public InvoiceService (IInvoiceRepository repo, IExternalSystemClient external, ILogger<InvoiceService> logger)
    {
        _repo = repo;
        _external = external;
        _logger = logger;
    }

    public async Task<Guid> CreateInvoiceAsync(CreateInvoiceRequest dto)
    {
        await ValidateInvoiceRequest(dto);

        var invoice = new Invoice(
                    invoiceNumber: dto.InvoiceNumber,
                    supplierId: dto.SupplierId,
                    invoiceDate: dto.InvoiceDate,
                    currency: dto.Currency);

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

    public async Task<InvoiceResponse> ExportInvoiceAsync(Guid id)
    {
        var invoice = await _repo.GetByIdAsync(id) ?? throw new NotFoundException<Invoice, Guid>(id);

        if (invoice.InvoiceExportStatus == InvoiceExportStatus.Exported)
        {
            throw new ExportErrorMessage("Invoice has already been exported.");
        }

        try
        {
            invoice.InvoiceExportStatus = InvoiceExportStatus.Exporting;
            await _repo.UpdateAsync(invoice);

            await _external.ExportAsync(invoice);

            invoice.InvoiceExportStatus = InvoiceExportStatus.Exported;
            invoice.ExportedAt = DateTime.UtcNow;
            invoice.ExportErrorMessage = null;
            await _repo.UpdateAsync(invoice);
        }
        catch (Exception ex)
        {
            invoice.InvoiceExportStatus = InvoiceExportStatus.ExportFailed;
            invoice.ExportErrorMessage = ex.Message;
            await _repo.UpdateAsync(invoice);
            throw;
        }

        return invoice.ToResponse();
    }

    public async Task<InvoiceResponse> GetByIdAsync(Guid id)
    {
        var invoice = await _repo.GetByIdAsync(id);

        return invoice is null ? throw new NotFoundException<Invoice, Guid>(id) : invoice.ToResponse();
    }

    public async Task<PagedResult<InvoiceResponse>> GetPagedAsync(
                            int page, 
                            int pageSize, 
                            Guid? supplierId,
                            InvoiceExportStatus? status, 
                            DateTime? from, 
                            DateTime? to)
    {
        FilterValidation(page, pageSize, from, to);

        var allInvoices = await _repo.GetAllAsync();

        IEnumerable<Invoice> query = allInvoices;

        if (supplierId.HasValue)
        {
            query = query.Where(x => x.SupplierId == supplierId.Value);
        }

        if (status.HasValue)
        {
            query = query.Where(x => x.InvoiceExportStatus == status);
        }

        if (from.HasValue)
        {
            query = query.Where(x => x.InvoiceDate >= from.Value);
        }

        if (to.HasValue)
        {
            query = query.Where(x => x.InvoiceDate <= to.Value);
        }

        var totalCount = query.Count();

        var paged = query
            .OrderByDescending(x => x.InvoiceDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var pagedResponses = paged.Select(x => x.ToResponse()).ToList();

        return new PagedResult<InvoiceResponse>
        {
            Items = pagedResponses,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    /// <summary>
    /// Filter validation to ensure that page and pageSize are valid, and that from date is not after to date.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <exception cref="DomainValidationException"></exception>
    private static void FilterValidation(int page, int pageSize, DateTime? from, DateTime? to)
    {
        if (page < 1)
        {
            throw new DomainValidationException("Page number must be at least 1");
        }
        if (pageSize < 1 || pageSize > 100)
        {
            throw new DomainValidationException("Page size must be between 1 and 100.");
        }
        if (page < 1 || pageSize < 1)
        {
            throw new DomainValidationException("Page and pageSize must be greater than 0.");
        }
        if (from.HasValue && to.HasValue && from > to)
        {
            throw new DomainValidationException("From date cannot be after To date.");
        }
    }

    /// <summary>
    ///  Validates the uniqueness of the invoice number + supplier id, and if supplier id is valid.
    /// </summary>
    /// <param name="dto"><see cref="CreateInvoiceRequest"/> Dto model to create invoice, needs to be validated.</param>
    /// <exception cref="BadRequestException">Exception is thrown if invalid</exception>
    private async Task ValidateInvoiceRequest(CreateInvoiceRequest dto)
    {
        if (dto.SupplierId == Guid.Empty)
        {
            throw new BadRequestException("Must provide a supplier id");
        }

        var supplierInvoiceExists = await _repo.GetByInvoiceNumberAsync(dto.InvoiceNumber, dto.SupplierId);

        if (supplierInvoiceExists)
        {
            throw new BadRequestException($"Invoice number {dto.InvoiceNumber} and {dto.SupplierId} must be unique.");
        }
    }
}