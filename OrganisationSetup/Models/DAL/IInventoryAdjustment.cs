using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class IInventoryAdjustment
{
    public int Id { get; set; }

    public Guid? GuID { get; set; }

    public string? Code { get; set; }

    public int? LocationId { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string? Description { get; set; }

    public int? ProductId { get; set; }

    public string? Attribute { get; set; }

    public int? InventoryAdjustmentTypeId { get; set; }

    public decimal UnitPurchasePrice { get; set; }

    public decimal UnitSalePrice { get; set; }

    public decimal QuantityIn { get; set; }

    public decimal QuantityOut { get; set; }

    public int? AdjustmentStatus { get; set; }

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
