using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class confClientProductSetting
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public int? ClientKEY { get; set; }

    public bool? EnableSKU { get; set; }

    public bool? EnableMachineNumber { get; set; }

    public bool? EnableFavorite { get; set; }

    public bool? EnableTaxSetting { get; set; }

    public bool? EnableAttribute { get; set; }

    public bool? EnableExpiry { get; set; }

    public bool? EnableATI { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public bool? Status { get; set; }

    public string? ProductTypeLabel { get; set; }

    public bool? EnableDepartment { get; set; }

}
