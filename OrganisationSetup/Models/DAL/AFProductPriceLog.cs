using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class AFProductPriceLog
{
    public int Id { get; set; }

    public Guid? GuID { get; set; }

    public string? Code { get; set; }

    public int? ProductId { get; set; }

    public int? ProductCombinationId { get; set; }

    public int? TierTypeId { get; set; }

    public decimal DefaultSalePrice { get; set; }

    public decimal MinimumSalePrice { get; set; }

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
