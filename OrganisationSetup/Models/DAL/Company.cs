using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class Company
{
    public int CompanyId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? AllowForms { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public virtual ICollection<AppUser> AppUser { get; set; } = new List<AppUser>();

    public virtual ICollection<Branch> Branch { get; set; } = new List<Branch>();
}
