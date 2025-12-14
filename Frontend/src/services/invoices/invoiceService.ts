import { httpGet, httpDelete } from "../http/httpClient.ts";
import type { InvoiceResponse } from "../../models/invoices.ts";
import type { PagedResult } from "../../models/common.ts";

export function getInvoices(page: number, pageSize: number) {
  return httpGet<PagedResult<InvoiceResponse>>("/invoices", {
    page: String(page),
    pageSize: String(pageSize),
  });
}

export function getInvoiceById(id: string) {
  return httpGet<InvoiceResponse>(`/invoices/${id}`);
}

export function deleteInvoice(id: string) {
  return httpDelete(`/invoices/${id}`);
}
