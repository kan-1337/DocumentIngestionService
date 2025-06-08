using InvoiceService.Invoices.Dtos;
using InvoiceService.Invoices.Models;

namespace InvoiceService.Invoices.Maping;
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
            Currency = "DKK",

            Lines = invoice.Lines.Select(l => new InvoiceLineResponse
            {
                Description = l.Description,
                Quantity = l.Quantity,
                UnitPrice = l.UnitPrice
            }).ToList()
        };
    }
}