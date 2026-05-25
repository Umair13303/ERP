using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class UserRight
{
    public int UserRightId { get; set; }

    public int UserId { get; set; }

    public int FormId { get; set; }

    public bool? CanView { get; set; }

    public bool? CanAdd { get; set; }

    public bool? CanEdit { get; set; }

    public bool? CanDelete { get; set; }

    public virtual AppForm Form { get; set; } = null!;

    public virtual AppUser User { get; set; } = null!;
}
