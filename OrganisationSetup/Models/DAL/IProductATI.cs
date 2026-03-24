using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class IProductATI
{
    public int Id { get; set; }

    public Guid? GuID { get; set; }

    public int? ProductId { get; set; }

    public int? InventoryAccountId { get; set; }

    public int? SaleRevenueAccountId { get; set; }

    public int? CostOfSaleAccountId { get; set; }

    public int? ItemTypeId { get; set; }

    public int? HSCodeId { get; set; }

    public int? SaleTaxTypeId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public int? DocumentType { get; set; }

    public int? DocumentStatus { get; set; }

    public bool? Status { get; set; }
}
