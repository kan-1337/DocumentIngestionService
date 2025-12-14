import type { InvoiceResponse } from "../../models/invoices.ts";

interface LineItemsProps {
  invoice: InvoiceResponse;
}

const COLUMN_SPAN = 6;

export function LineItems({ invoice }: LineItemsProps) {
  if (!invoice.lines || invoice.lines.length === 0) {
    return null;
  }

  return (
    <tr className="expanded-row">
      <td colSpan={COLUMN_SPAN}>
        <div className="line-items">
          <h4>Line Items</h4>
          <table className="line-items-table">
            <thead>
              <tr>
                <th>Description</th>
                <th>Quantity</th>
                <th>Unit Price</th>
                <th>Total</th>
              </tr>
            </thead>
            <tbody>
              {invoice.lines.map((line, idx) => (
                <tr key={idx}>
                  <td>{line.description}</td>
                  <td>{line.quantity}</td>
                  <td>{invoice.currency} {line.unitPrice.toFixed(2)}</td>
                  <td>{invoice.currency} {(line.quantity * line.unitPrice).toFixed(2)}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </td>
    </tr>
  );
}
