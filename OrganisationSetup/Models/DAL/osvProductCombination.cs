using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class osvProductCombination
{
    public int Id { get; set; }

    public Guid? GuID { get; set; }

    public int? RefDocumentType { get; set; }

    public int? ProductId { get; set; }

    public string? Attribute { get; set; }

    public bool? Status { get; set; }
}
