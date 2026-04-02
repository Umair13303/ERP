using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class vAccountType
{
    public int Id { get; set; }

    public string? ShortCode { get; set; }

    public string? Description { get; set; }

    public bool? Status { get; set; }
}
