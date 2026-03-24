using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class AFInvoiceProduct
{
    public int Id { get; set; }

    public Guid? GuID { get; set; }

    public int? InvoiceId { get; set; }

    public int? ProductId { get; set; }

    public decimal Quantity { get; set; }

    public decimal ActualAmount { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal ChargedAmount { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public int? DocumentType { get; set; }

    public int? DocumentStatus { get; set; }

    public bool? Status { get; set; }
}
