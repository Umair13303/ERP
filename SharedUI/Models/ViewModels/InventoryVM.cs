using System;

namespace SharedUI.Models.ViewModels
{
    public class StockOnHandRow
    {
        public int ProductId { get; set; }
        public string? Product { get; set; }
        public decimal OnHandQty { get; set; }
        public decimal AvgUnitCost { get; set; }
    }

    public class StockLedgerRow
    {
        public DateTime TransactionDate { get; set; }
        public string? RefCode { get; set; }
        public string? Description { get; set; }
        public decimal InQty { get; set; }
        public decimal OutQty { get; set; }
        public decimal UnitCost { get; set; }
    }
}

