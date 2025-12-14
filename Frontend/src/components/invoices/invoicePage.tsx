import { useEffect, useState } from "react";
import { getInvoices } from "../../services/invoices/invoiceService.ts";
import type { InvoiceResponse } from "../../models/invoices.ts";
import { InvoicesTable } from "./invoiceTable.tsx";

export function InvoicePage() {
  const [items, setItems] = useState<InvoiceResponse[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    (async () => {
      try {
        setLoading(true);
        setError(null);

        const result = await getInvoices(1, 10);
        setItems(result.items);
      } catch (e) {
        setError(e instanceof Error ? e.message : "Unknown error");
      } finally {
        setLoading(false);
      }
    })();
  }, []);

  if (loading) return <p>Loading invoices…</p>;
  if (error) return <p style={{ color: "#ff8080" }}>{error}</p>;

  return <InvoicesTable invoices={items} />;
}
