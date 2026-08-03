using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class IProductCCE
{
    public int Id { get; set; }
    public Guid? GuID { get; set; }
    public int? RefDocumentType { get; set; }
    public int? ProductId { get; set; }
    public string? Description { get; set; }
    public string? QRCode { get; set; }
    public DateTime? CreatedOn { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public int? UpdatedBy { get; set; }
    public int? DocumentType { get; set; }
    public int? DocumentStatus { get; set; }
    public bool? Status { get; set; }
}
