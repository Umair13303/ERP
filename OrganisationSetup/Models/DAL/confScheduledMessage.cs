using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class confScheduledMessage
{
    public int Id { get; set; }

    public int? RefDocumentType { get; set; }

    public Guid? RefDocumentGuID { get; set; }

    public DateTime? ScheduledOn { get; set; }

    public string? Contact { get; set; }

    public string? Message { get; set; }

    public bool? IsSent { get; set; }

    public string? APIResponse { get; set; }
}
