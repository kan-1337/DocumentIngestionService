﻿namespace DocumentIngestion.Api.Invoices.Dtos;
public class CreateInvoiceLineRequest
{
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
