using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class vInventoryAdjustmentType
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public bool IsPurchasePrice { get; set; }

    public bool IsSalePrice { get; set; }

    public bool IsQuantityIn { get; set; }

    public bool IsQuantityOut { get; set; }

    public bool? IsAutoPriceUpdate { get; set; }

    public bool? Status { get; set; }
}
