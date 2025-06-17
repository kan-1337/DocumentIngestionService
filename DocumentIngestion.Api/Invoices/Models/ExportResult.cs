namespace DocumentIngestion.Api.Invoices.Models;
public enum ExportResult
{
    Success = 0,
    NotFound = 1,
    AlreadyExported = 2,
    Failed = 3
}
