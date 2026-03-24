using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class vCity
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public int? CountryId { get; set; }
}
