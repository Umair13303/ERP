using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class vCountry
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string? Code { get; set; }

    public string? CallingCode { get; set; }

    public bool? Status { get; set; }
}
