using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class Branch
{
    public int BranchId { get; set; }

    public int CompanyId { get; set; }

    public string BranchName { get; set; } = null!;

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public virtual ICollection<AppUser> AppUser { get; set; } = new List<AppUser>();

    public virtual Company Company { get; set; } = null!;
}
