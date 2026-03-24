using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class osvChartOfAccount
{
    public int Id { get; set; }

    public Guid? GuID { get; set; }

    public string? Code { get; set; }

    public string? Description { get; set; }

    public int? AccountCategoryId { get; set; }

    public int? FinancialStatementId { get; set; }

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
