using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class AFInventoryLedger
{
    public int Id { get; set; }

    public Guid? GuID { get; set; }

    public string? Code { get; set; }

    public int? LocationId { get; set; }

    public DateTime? TransactionDate { get; set; }

    public int? ProductId { get; set; }

    public int? ProductCombinationId { get; set; }

    public int? RefDocumentType { get; set; }

    public int? RefDocumentId { get; set; }

    public string? Description { get; set; }

    public decimal QuantityIn { get; set; }

    public decimal QuantityOut { get; set; }

    public decimal UnitPurchasePrice { get; set; }

    public decimal UnitSalePrice { get; set; }

    public decimal Debit { get; set; }

    public decimal Credit { get; set; }

    public string? Batch { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public int? ReconcillationStatus { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public int? DocumentType { get; set; }

    public int? DocumentStatus { get; set; }

    public bool? Status { get; set; }

    public int? BranchId { get; set; }

    public int? CompanyId { get; set; }
}
