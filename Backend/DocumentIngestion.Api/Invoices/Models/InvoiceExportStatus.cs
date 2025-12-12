namespace DocumentIngestion.Api.Invoices.Models;
public enum InvoiceExportStatus
{
    NotExported = 0,
    Exporting = 1,
    Exported = 2,
    ExportFailed = 3
}
