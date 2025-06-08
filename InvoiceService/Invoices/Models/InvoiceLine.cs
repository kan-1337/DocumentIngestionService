namespace DocumentIngestion.Api.Invoices.Models;
public class InvoiceLine
{
    public string Description { get; }
    public int Quantity { get; }
    public decimal UnitPrice { get; }
    public decimal Total => Quantity * UnitPrice;

    public InvoiceLine(string description, int quantity, decimal unitPrice)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentNullException("description");
        }
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException("Quantity must be positive");
        }
        if (unitPrice < 0)
        {
            throw new ArgumentOutOfRangeException("Unit price cannot be negative");
        }

        Description = description;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}
