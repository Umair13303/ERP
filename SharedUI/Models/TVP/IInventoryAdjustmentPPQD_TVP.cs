using SharedUI.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUI.Models.TVP
{
    public class IInventoryAdjustmentPPQD_TVP
    {
        public Guid? GuID { get; set; }
        public int? AdjustmentId { get; set; }
        public int? ProductId { get; set; }
        public string? Attribute { get; set; }
        public int? ProductCombinationId { get; set; }
        public decimal UnitPurchasePrice { get; set; }
        public decimal UnitSalePrice { get; set; }
        public decimal QuantityIn { get; set; }
        public decimal QuantityOut { get; set; }
        public string? Batch { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DocumentType { get; set; } = (int?)SharedUI.Models.Enums.DocumentType.inventoryAdjustmentProductInformation;
        public int? DocumentStatus { get; set; } = (int?)SharedUI.Models.Enums.DocumentStatus.active;
        public bool? Status { get; set; } = true;
    }
}