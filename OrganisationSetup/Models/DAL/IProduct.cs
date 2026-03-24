using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class IProduct
{
    public int Id { get; set; }

    public Guid? GuID { get; set; }

    public string? Code { get; set; }

    public string? Description { get; set; }

    public string? MachineNumber { get; set; }

    public string? SKU { get; set; }

    public string? AdditionalDetail { get; set; }

    public string? AttributeIds { get; set; }

    public int? BrandId { get; set; }

    public bool? IsFavorite { get; set; }

    public bool? IsSaleTaxExclusive { get; set; }

    public int? DepartmentId { get; set; }

    public int? SectionId { get; set; }

    public int? CategoryId { get; set; }

    public int? SubCategoryId { get; set; }

    public decimal CriticalLimit { get; set; }

    public int? SaleUnitId { get; set; }

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
