using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class ACUser
{
    public int Id { get; set; }

    public Guid? GuID { get; set; }

    public string? Code { get; set; }

    public string? Description { get; set; }

    public string? Password { get; set; }

    public string? Contact { get; set; }

    public string? Email { get; set; }

    public int? EmployeeId { get; set; }

    public int? RoleId { get; set; }

    public string? AllowedBranchIds { get; set; }

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
