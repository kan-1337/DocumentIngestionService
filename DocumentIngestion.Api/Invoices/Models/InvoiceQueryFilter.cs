using System.ComponentModel.DataAnnotations;

namespace DocumentIngestion.Api.Invoices.Models;
public class InvoiceQueryFilter
{
    /// <summary>
    /// The page number to retrieve. Must be greater than 0.
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than 0.")]
    public int Page { get; set; }

    /// <summary>
    /// The number of invoices per page. Must be greater than 0.
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "PageSize must be greater than 0.")]
    public int PageSize { get; set; }

    /// <summary>
    /// Optional. Filter by the supplier's unique identifier.
    /// </summary>
    public Guid? SupplierId { get; set; }

    /// <summary>
    /// Optional. Filter by invoice export status (e.g., Draft, Exported).
    /// </summary>
    public InvoiceExportStatus? Status { get; set; }

    /// <summary>
    /// Optional. Filter invoices with an issue date from this date (inclusive).
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// Optional. Filter invoices with an issue date up to this date (inclusive).
    /// </summary>
    public DateTime? To { get; set; }
}
