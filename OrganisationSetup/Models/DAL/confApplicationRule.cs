using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class confApplicationRule
{
    public int Id { get; set; }

    public int? ClientKEY { get; set; }

    public int? WalkInCustomerLimit { get; set; }
}
