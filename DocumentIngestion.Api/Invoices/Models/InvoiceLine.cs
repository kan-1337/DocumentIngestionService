using Shared.Common.Exceptions;

namespace DocumentIngestion.Api.Invoices.Models;
public class InvoiceLine
{
    public string? Description { get; }
    public int Quantity { get; }
    public decimal UnitPrice { get; }
    public decimal Total => Quantity * UnitPrice;

    public InvoiceLine(string? description, int quantity, decimal unitPrice)
    {
        if (quantity < 0)
        {
            throw new BadRequestException("Quantity cannot be negative.");
        }
        if (unitPrice < 0)
        {
            throw new BadRequestException("Unit price cannot be negative.");
        }

        Description = description is not null ? description : "";
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}
