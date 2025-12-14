import type { InvoiceResponse } from "../../models/invoices.ts";
import { STATUS_LABELS, STATUS_COLORS } from "../../models/invoiceConstants.ts";

interface InvoiceRowProps {
  invoice: InvoiceResponse;
  isExpanded: boolean;
  onToggle: () => void;
}

export function InvoiceRow({ invoice, isExpanded, onToggle }: InvoiceRowProps) {
  return (
    <tr onClick={onToggle} className="invoice-row">
      <td className="expand-cell">
        <span className={`expand-icon ${isExpanded ? "expanded" : ""}`}></span>
      </td>
      <td className="invoice-number">{invoice.invoiceNumber}</td>
      <td>{new Date(invoice.invoiceDate).toLocaleDateString()}</td>
      <td className="amount">
        {invoice.currency} {invoice.totalAmount.toFixed(2)}
      </td>
      <td>
        <span
          className={`status-badge ${STATUS_COLORS[invoice.invoiceExportStatus]}`}
        >
          {STATUS_LABELS[invoice.invoiceExportStatus]}
        </span>
      </td>
      <td className="supplier-id">{invoice.supplierId}</td>
    </tr>
  );
}
