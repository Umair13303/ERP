using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class vHSCode
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string? AdditionalDetail { get; set; }

    public bool? Status { get; set; }
}
