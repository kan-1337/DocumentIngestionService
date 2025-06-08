namespace InvoiceService.Invoices.Models;
public enum InvoiceStatus
{
    Draft = 0,
    Submitted = 1,
    Booked = 2,
    Paid = 3,
    Dunning = 4,
}
