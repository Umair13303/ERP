using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class AppForm
{
    public int FormId { get; set; }

    public string FormCode { get; set; } = null!;

    public string FormName { get; set; } = null!;

    public string? ModuleName { get; set; }

    public virtual ICollection<UserRight> UserRight { get; set; } = new List<UserRight>();
}
