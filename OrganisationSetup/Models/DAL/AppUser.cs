using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class AppUser
{
    public int UserId { get; set; }

    public int CompanyId { get; set; }

    public int? BranchId { get; set; }

    public string UserName { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public bool? IsSuperAdmin { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public virtual Branch? Branch { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<UserRight> UserRight { get; set; } = new List<UserRight>();
}
