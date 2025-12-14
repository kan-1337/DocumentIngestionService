import type { InvoiceResponse } from "../../models/invoices.ts";

export function InvoicesTable({ invoices }: { invoices: InvoiceResponse[] }) {
  return (
    <table className="table">
      <thead>
        <tr>
          <th>Invoice #</th>
          <th>Date</th>
          <th>Total</th>
          <th>Currency</th>
          <th>Status</th>
          <th>Supplier</th>
        </tr>
      </thead>
      <tbody>
        {invoices.map((inv) => (
          <tr key={inv.id}>
            <td>{inv.invoiceNumber}</td>
            <td>{new Date(inv.invoiceDate).toLocaleDateString()}</td>
            <td>{inv.totalAmount.toFixed(2)}</td>
            <td>{inv.currency}</td>
            <td>{inv.invoiceExportStatus}</td>
            <td className="mono">{inv.supplierId}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
