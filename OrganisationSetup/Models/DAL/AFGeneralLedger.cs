using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class AFGeneralLedger
{
    public long Id { get; set; }

    public DateTime TransactionDate { get; set; }

    public int LocationId { get; set; }

    public int AccountId { get; set; }

    public int? PartyId { get; set; }

    public int? RefDocumentType { get; set; }

    public int? RefDocumentId { get; set; }

    public string? DocumentCode { get; set; }

    public string? Description { get; set; }

    public decimal Debit { get; set; }

    public decimal Credit { get; set; }

    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }

    public int? BranchId { get; set; }

    public int? CompanyId { get; set; }
}
