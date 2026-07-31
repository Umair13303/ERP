using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class CSDefaultChartOfAccount
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public int? AccountCategoryId { get; set; }

    public int? DefaultChartOfAccountId { get; set; }

    public bool? Status { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
    public int CompanyId { get; set; }
}
