using DocumentIngestion.Api.Invoices.Dtos;
using DocumentIngestion.Api.Invoices.Models;

namespace DocumentIngestion.Api.Invoices.Maping;
public static class InvoiceMappings
{
    public static InvoiceResponse ToResponse(this Invoice invoice)
    {
        return new InvoiceResponse
        {
            Id = invoice.Id,
            InvoiceNumber = invoice.InvoiceNumber,
            SupplierId = invoice.SupplierId,
            InvoiceDate = invoice.InvoiceDate,
            TotalAmount = invoice.TotalAmount,
            Currency = !string.IsNullOrWhiteSpace(invoice.Currency) ? invoice.Currency : "DKK", // Should probably not be null or empty, but default to DKK if it is for demonstration purposes
            InvoiceExportStatus = invoice.InvoiceExportStatus,
            Lines = [.. invoice.Lines.Select(l => new InvoiceLineResponse
            {
                Description = l.Description,
                Quantity = l.Quantity,
                UnitPrice = l.UnitPrice,
                Total = l.Total,
            })]
        };
    }
}