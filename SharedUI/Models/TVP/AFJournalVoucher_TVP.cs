using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUI.Models.TVP
{
    public partial class AFJournalVoucher_TVP
    {
        public int Id { get; set; }
        public Guid? GuID { get; set; }
        public string? Code { get; set; }
        public int? LocationId { get; set; }
        public int? RefDocumentType { get; set; }
        public Guid? RefDocumentGuID { get; set; }
        public string? Description { get; set; }
        public int? ChartOfAccountId { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DocumentType { get; set; }
        public int? DocumentStatus { get; set; }
        public bool? Status { get; set; }
        public int? BranchId { get; set; }
        public int? CompanyId { get; set; }
    }
}
