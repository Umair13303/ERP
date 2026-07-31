using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class AFStockLedger
{
    public long Id { get; set; }

    public DateTime TransactionDate { get; set; }

    public int LocationId { get; set; }

    public int ProductId { get; set; }

    public int? ProductCombinationId { get; set; }

    public string? BatchNo { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public decimal QtyIn { get; set; }

    public decimal QtyOut { get; set; }

    public decimal UnitCost { get; set; }

    public decimal UnitPrice { get; set; }

    public int RefDocumentType { get; set; }

    public int RefDocumentId { get; set; }

    public string? DocumentCode { get; set; }

    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }
}
