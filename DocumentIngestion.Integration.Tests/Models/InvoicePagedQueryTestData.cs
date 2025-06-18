using DocumentIngestion.Api.Invoices.Models;

namespace DocumentIngestion.Integration.Tests.Models;

public class InvoicePagedQueryTestData
{
    public string TestName { get; set; } = "";
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int ExpectedCount { get; set; }
    public Guid? SupplierId { get; set; } = null;
    public InvoiceExportStatus? Status { get; set; } = null;
}
