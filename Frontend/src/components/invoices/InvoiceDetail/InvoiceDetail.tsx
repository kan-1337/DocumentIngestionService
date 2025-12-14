import { useEffect, useState } from "react";
import { getInvoiceById } from "../../../services/invoices/invoiceService.ts";
import type { InvoiceResponse } from "../../../models/invoices.ts";
import { STATUS_LABELS } from "../../../models/invoiceConstants.ts";
import "./InvoiceDetail.css";
import "../InvoiceTable/InvoiceTable.css";

interface InvoiceDetailProps {
  invoiceId: string;
  onBack: () => void;
}

export function InvoiceDetail({ invoiceId, onBack }: InvoiceDetailProps) {
  const [invoice, setInvoice] = useState<InvoiceResponse | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    (async () => {
      try {
        setLoading(true);
        setError(null);
        const result = await getInvoiceById(invoiceId);
        setInvoice(result);
      } catch (e) {
        setError(e instanceof Error ? e.message : "Unknown error");
      } finally {
        setLoading(false);
      }
    })();
  }, [invoiceId]);

  if (loading) {
    return (
      <div className="loading-state">
        <p>Loading invoice...</p>
      </div>
    );
  }

  if (error) {
    return (
      <div className="error-state">
        <p>{error}</p>
        <button onClick={onBack}>Back to List</button>
      </div>
    );
  }

  if (!invoice) {
    return null;
  }

  return (
    <div className="invoice-detail">
      <div className="detail-header">
        <button onClick={onBack} className="back-button">
          Back to List
        </button>
        <h2>Invoice Details</h2>
      </div>

      <div className="detail-card">
        <div className="detail-row">
          <span className="detail-label">Invoice Number:</span>
          <span className="detail-value">{invoice.invoiceNumber}</span>
        </div>

        <div className="detail-row">
          <span className="detail-label">Date:</span>
          <span className="detail-value">
            {new Date(invoice.invoiceDate).toLocaleDateString()}
          </span>
        </div>

        <div className="detail-row">
          <span className="detail-label">Total Amount:</span>
          <span className="detail-value">
            {invoice.currency} {invoice.totalAmount.toFixed(2)}
          </span>
        </div>

        <div className="detail-row">
          <span className="detail-label">Status:</span>
          <span className="detail-value">
            {STATUS_LABELS[invoice.invoiceExportStatus]}
          </span>
        </div>

        <div className="detail-row">
          <span className="detail-label">Supplier ID:</span>
          <span className="detail-value supplier-id">{invoice.supplierId}</span>
        </div>
      </div>

      {invoice.lines && invoice.lines.length > 0 && (
        <div className="detail-section">
          <h3>Line Items</h3>
          <table className="invoice-table">
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
                  <td>
                    {invoice.currency} {line.unitPrice.toFixed(2)}
                  </td>
                  <td>
                    {invoice.currency}{" "}
                    {(line.quantity * line.unitPrice).toFixed(2)}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}
