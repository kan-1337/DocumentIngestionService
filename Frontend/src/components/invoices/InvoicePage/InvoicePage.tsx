import { useEffect, useState } from "react";
import {
  getInvoices,
  deleteInvoice,
} from "../../../services/invoices/invoiceService.ts";
import type { InvoiceResponse } from "../../../models/invoices.ts";
import { InvoicesTable } from "../InvoiceTable/InvoiceTable.tsx";
import { InvoiceDetail } from "../InvoiceDetail/InvoiceDetail.tsx";
import { useAuth } from "../../../contexts/AuthContext";

const DEFAULT_PAGE = 1;
const DEFAULT_PAGE_SIZE = 10;

export function InvoicePage() {
  const { isAdmin } = useAuth();
  const [items, setItems] = useState<InvoiceResponse[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [selectedInvoiceId, setSelectedInvoiceId] = useState<string | null>(
    null,
  );

  useEffect(() => {
    (async () => {
      try {
        setLoading(true);
        setError(null);

        const result = await getInvoices(DEFAULT_PAGE, DEFAULT_PAGE_SIZE);
        setItems(result.items);
      } catch (e) {
        setError(e instanceof Error ? e.message : "Unknown error");
      } finally {
        setLoading(false);
      }
    })();
  }, []);

  const handleDelete = async (id: string) => {
    if (!confirm("Are you sure you want to delete this invoice?")) {
      return;
    }

    try {
      await deleteInvoice(id);
      setItems(items.filter((invoice) => invoice.id !== id));
    } catch (e) {
      alert(
        `Failed to delete invoice: ${e instanceof Error ? e.message : "Unknown error"}`,
      );
    }
  };

  if (selectedInvoiceId) {
    return (
      <InvoiceDetail
        invoiceId={selectedInvoiceId}
        onBack={() => setSelectedInvoiceId(null)}
      />
    );
  }

  return (
    <div className="invoice-page">
      {loading && (
        <div className="loading-state">
          <p>Loading invoices...</p>
        </div>
      )}

      {error && (
        <div className="error-state">
          <p>{error}</p>
        </div>
      )}

      {!loading && !error && (
        <InvoicesTable
          invoices={items}
          onSelectInvoice={setSelectedInvoiceId}
          onDeleteInvoice={isAdmin ? handleDelete : undefined}
        />
      )}
    </div>
  );
}
