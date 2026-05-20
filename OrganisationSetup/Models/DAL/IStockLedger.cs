using System;

namespace OrganisationSetup.Models.DAL;

public partial class IStockLedger
{
    public int Id { get; set; }

    public Guid? GuID { get; set; }

    public int? LocationId { get; set; }

    public DateTime? TransactionDate { get; set; }

    public int? ProductId { get; set; }

    public int? RefDocumentType { get; set; }

    public int? RefDocumentId { get; set; }

    public string? Description { get; set; }

    public decimal QuantityIn { get; set; }

    public decimal QuantityOut { get; set; }

    public decimal UnitCost { get; set; }

    public decimal RunningBalance { get; set; }

    public decimal RunningValue { get; set; }

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