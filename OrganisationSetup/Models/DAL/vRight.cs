using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class vRight
{
    public int Id { get; set; }

    public string? Menu { get; set; }

    public string? SubMenu { get; set; }

    public string? FormName { get; set; }

    public string? DisplayName { get; set; }

    public string? Purpose { get; set; }

    public string? RoleIds { get; set; }

    public int? Priority { get; set; }

    public bool? IsDisplayAllowed { get; set; }

    public bool? Status { get; set; }
}
