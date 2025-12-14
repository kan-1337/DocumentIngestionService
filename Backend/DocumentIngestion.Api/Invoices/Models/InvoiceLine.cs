using Shared.Common.Exceptions;

namespace DocumentIngestion.Api.Invoices.Models;
public class InvoiceLine
{
    private InvoiceLine() { }
    public Guid Id { get; private set; }
    public Guid InvoiceId { get; private set; }
    public string Description { get; private set; } = "";
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
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

        Description = description ?? "";
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}
