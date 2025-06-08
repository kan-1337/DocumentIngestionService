namespace InvoiceService.Invoices.Models;
public enum InvoiceStatus
{
    Draft = 0,
    Booked = 1,
    Paid = 2,
    Dunning = 3,
}
