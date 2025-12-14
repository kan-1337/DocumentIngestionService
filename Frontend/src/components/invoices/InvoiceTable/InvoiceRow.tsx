import { useState, useRef, useEffect } from "react";
import type { InvoiceResponse } from "../../../models/invoices.ts";
import {
  STATUS_LABELS,
  STATUS_COLORS,
} from "../../../models/invoiceConstants.ts";

interface InvoiceRowProps {
  invoice: InvoiceResponse;
  isExpanded: boolean;
  onToggle: () => void;
  onView: () => void;
  onDelete?: () => void;
  isDropdownOpen: boolean;
  onDropdownToggle: () => void;
}

export function InvoiceRow({
  invoice,
  isExpanded,
  onToggle,
  onView,
  onDelete,
  isDropdownOpen,
  onDropdownToggle,
}: InvoiceRowProps) {
  const [dropdownPosition, setDropdownPosition] = useState({ top: 0, left: 0 });
  const buttonRef = useRef<HTMLButtonElement>(null);

  useEffect(() => {
    if (isDropdownOpen && buttonRef.current) {
      const rect = buttonRef.current.getBoundingClientRect();
      setDropdownPosition({
        top: rect.bottom + window.scrollY,
        left: rect.right + window.scrollX - 120,
      });
    }
  }, [isDropdownOpen]);

  return (
    <>
      <tr className="invoice-row">
        <td className="expand-cell" onClick={onToggle}>
          {isExpanded ? "-" : "+"}
        </td>
        <td onClick={onToggle}>{invoice.invoiceNumber}</td>
        <td onClick={onToggle}>
          {new Date(invoice.invoiceDate).toLocaleDateString()}
        </td>
        <td className="amount" onClick={onToggle}>
          {invoice.currency} {invoice.totalAmount.toFixed(2)}
        </td>
        <td onClick={onToggle}>
          <span
            className={`status-badge ${STATUS_COLORS[invoice.invoiceExportStatus]}`}
          >
            {STATUS_LABELS[invoice.invoiceExportStatus]}
          </span>
        </td>
        <td className="supplier-id" onClick={onToggle}>
          {invoice.supplierId}
        </td>
        <td className="actions-cell" onClick={(e) => e.stopPropagation()}>
          <button
            ref={buttonRef}
            onClick={(e) => {
              e.stopPropagation();
              onDropdownToggle();
            }}
            className="dropdown-button"
          >
            •••
          </button>
        </td>
      </tr>
      {isDropdownOpen && (
        <div
          className="dropdown-menu-fixed"
          style={{
            top: `${dropdownPosition.top}px`,
            left: `${dropdownPosition.left}px`,
          }}
        >
          <button
            onClick={(e) => {
              e.stopPropagation();
              onView();
              onDropdownToggle();
            }}
            className="dropdown-item"
          >
            View
          </button>
          {onDelete && (
            <button
              onClick={(e) => {
                e.stopPropagation();
                onDelete();
                onDropdownToggle();
              }}
              className="dropdown-item delete"
            >
              Delete
            </button>
          )}
        </div>
      )}
    </>
  );
}
