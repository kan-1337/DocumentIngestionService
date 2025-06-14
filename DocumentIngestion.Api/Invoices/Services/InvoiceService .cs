﻿using DocumentIngestion.Api.ExternalSystems;
using DocumentIngestion.Api.Invoices.Dtos;
using DocumentIngestion.Api.Invoices.Models;
using DocumentIngestion.Api.Invoices.Repositories;
using Shared.Common.Exceptions;

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


    public async Task<Invoice> GetByIdAsync(Guid id)
    {
        var invoice = await _repo.GetByIdAsync(id);

        return invoice is null ? throw new NotFoundException<Invoice, Guid>(id) : invoice;
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