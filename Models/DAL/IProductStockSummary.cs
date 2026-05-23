using System;

namespace OrganisationSetup.Models.DAL;

/// <summary>
/// IProductStockSummary - Real-time stock summary for variant-based products
/// Composite Key: (ProductId, LocationId, VariantKey, CompanyId)
/// Purpose: O(1) lookup for costing calculations instead of scanning ledger
/// </summary>
public partial class IProductStockSummary
{
    /// <summary>Identity</summary>
    public int Id { get; set; }

    /// <summary>Global unique identifier</summary>
    public Guid? GuID { get; set; }

    /// <summary>Product ID - part of composite key</summary>
    public int ProductId { get; set; }

    /// <summary>Location/Branch ID - part of composite key</summary>
    public int LocationId { get; set; }

    /// <summary>
    /// Variant Key - Normalized variant combination
    /// Format: "1=Black|2=Large" (always sorted alphabetically)
    /// Default: "DEFAULT" for products without variants
    /// Part of composite key
    /// </summary>
    public string VariantKey { get; set; } = "";

    /// <summary>
    /// Human-readable variant display
    /// Example: "Black, Large" for UI display
    /// </summary>
    public string? VariantDisplay { get; set; }

    /// <summary>Current quantity on hand for this variant</summary>
    public decimal CurrentQty { get; set; } = 0;

    /// <summary>Current total value = CurrentQty * Average Unit Cost</summary>
    public decimal CurrentValue { get; set; } = 0;

    /// <summary>WAC: Weighted Average Cost per unit</summary>
    public decimal WAC_UnitCost { get; set; } = 0;

    /// <summary>WAC: Total value for this variant</summary>
    public decimal WAC_TotalValue { get; set; } = 0;

    /// <summary>
    /// FIFO: JSON serialized queue of batches
    /// Each batch: {Qty, UnitCost, AcquisitionDate}
    /// Consumed oldest first
    /// </summary>
    public string? FIFOBatchQueue { get; set; }

    /// <summary>
    /// LIFO: JSON serialized stack of batches
    /// Each batch: {Qty, UnitCost, AcquisitionDate}
    /// Consumed newest first
    /// </summary>
    public string? LIFOBatchStack { get; set; }

    /// <summary>Last update timestamp</summary>
    public DateTime? LastUpdated { get; set; }

    /// <summary>Creation timestamp</summary>
    public DateTime? CreatedOn { get; set; }

    /// <summary>Last modified timestamp</summary>
    public DateTime? UpdatedOn { get; set; }

    /// <summary>Branch ID for filtering</summary>
    public int? BranchId { get; set; }

    /// <summary>Company ID - part of composite key</summary>
    public int CompanyId { get; set; }
}
