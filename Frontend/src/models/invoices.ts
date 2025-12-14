export const InvoiceExportStatus = {
  NotExported: 0,
  Exporting: 1,
  Exported: 2,
  ExportFailed: 3,
} as const;

export type InvoiceLineResponse = {
  description: string;
  quantity: number;
  unitPrice: number;
};

export type InvoiceResponse = {
  id: string;
  invoiceNumber: string;
  supplierId: string;
  invoiceDate: string;
  totalAmount: number;
  currency: string;
  invoiceExportStatus: InvoiceExportStatus;
  lines: InvoiceLineResponse[];
};

export type InvoiceExportStatus =
  (typeof InvoiceExportStatus)[keyof typeof InvoiceExportStatus];
