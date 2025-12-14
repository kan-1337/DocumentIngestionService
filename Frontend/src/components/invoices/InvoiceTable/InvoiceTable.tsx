import { useState } from "react";
import type { InvoiceResponse } from "../../../models/invoices.ts";
import { InvoiceRow } from "./InvoiceRow.tsx";
import { LineItems } from "./LineItems.tsx";
import "./InvoiceTable.css";

interface InvoicesTableProps {
  invoices: InvoiceResponse[];
  onSelectInvoice: (id: string) => void;
  onDeleteInvoice: (id: string) => void;
}

export function InvoicesTable({
  invoices,
  onSelectInvoice,
  onDeleteInvoice,
}: InvoicesTableProps) {
  const [expandedRow, setExpandedRow] = useState<string | null>(null);

  if (invoices.length === 0) {
    return (
      <div className="empty-state">
        <p>No invoices found</p>
      </div>
    );
  }

  const toggleRow = (id: string) => {
    setExpandedRow(expandedRow === id ? null : id);
  };

  return (
    <div className="invoice-table-container">
      <table className="invoice-table">
        <thead>
          <tr>
            <th></th>
            <th>Invoice #</th>
            <th>Date</th>
            <th>Total Amount</th>
            <th>Status</th>
            <th>Supplier ID</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {invoices.map((invoice) => (
            <>
              <InvoiceRow
                key={invoice.id}
                invoice={invoice}
                isExpanded={expandedRow === invoice.id}
                onToggle={() => toggleRow(invoice.id)}
                onView={() => onSelectInvoice(invoice.id)}
                onDelete={() => onDeleteInvoice(invoice.id)}
              />
              {expandedRow === invoice.id && <LineItems invoice={invoice} />}
            </>
          ))}
        </tbody>
      </table>
    </div>
  );
}
