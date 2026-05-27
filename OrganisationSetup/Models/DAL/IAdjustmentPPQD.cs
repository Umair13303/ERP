using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class IAdjustmentPPQD
{
    public int Id { get; set; }

    public Guid? GuID { get; set; }

    public int? AdjustmentId { get; set; }

    public int? ProductId { get; set; }

    public int? ProductCombinationId { get; set; }

    public decimal UnitPurchasePrice { get; set; }

    public decimal UnitSalePrice { get; set; }

    public decimal QuantityIn { get; set; }

    public decimal QuantityOut { get; set; }
    public string? Batch { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public int? DocumentType { get; set; }

    public int? DocumentStatus { get; set; }

    public bool? Status { get; set; }
}
