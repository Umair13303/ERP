using System;

namespace SharedUI.Models.TVP
{
    /// <summary>
    /// Stock Ledger TVP with Variant Support
    /// Used for batch inserting stock transactions
    /// </summary>
    public class IStockLedger_TVP
    {
        public int Id { get; set; }
        public Guid? GuID { get; set; }
        public int? LocationId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public int? ProductId { get; set; }
        public int? RefDocumentType { get; set; }  // 26=Adjustment, 16=Invoice, 22=Bill
        public int? RefDocumentId { get; set; }
        public string? Description { get; set; }
        public decimal InQty { get; set; }
        public decimal OutQty { get; set; }
        public decimal UnitCost { get; set; }
        public int? CostingModeId { get; set; }
        
        /// <summary>⭐ NEW: Variant key for variant-based tracking</summary>
        public string? VariantKey { get; set; }
        
        /// <summary>⭐ NEW: Human-readable variant display</summary>
        public string? VariantDisplay { get; set; }
        
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DocumentType { get; set; }
        public int? DocumentStatus { get; set; }
        public bool? Status { get; set; }
        public int? BranchId { get; set; }
        public int? CompanyId { get; set; }

        /// <summary>Validate before insert</summary>
        public bool IsValid()
        {
            if (!ProductId.HasValue || ProductId <= 0)
                throw new ArgumentException("ProductId is required and must be > 0");

            if (!LocationId.HasValue || LocationId <= 0)
                throw new ArgumentException("LocationId is required and must be > 0");

            if (InQty == 0 && OutQty == 0)
                throw new ArgumentException("InQty and OutQty cannot both be 0");

            if (UnitCost < 0)
                throw new ArgumentException("UnitCost cannot be negative");

            // Set default variant if not provided
            if (string.IsNullOrWhiteSpace(VariantKey))
                VariantKey = "DEFAULT";

            return true;
        }
    }
}
