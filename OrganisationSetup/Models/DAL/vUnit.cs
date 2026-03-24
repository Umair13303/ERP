using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class vUnit
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public bool? IsScalar { get; set; }

    public bool? Status { get; set; }
}
