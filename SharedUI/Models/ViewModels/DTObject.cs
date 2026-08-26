using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SharedUI.Models.ViewModels.DTObject;

namespace SharedUI.Models.ViewModels
{
    public class DTObject
    {
        public class SubCategory_List
        {
            public int? SubCategoryId { get; set; }
            public string? Category { get; set; }
            public string? SubCategory { get; set; }
        }
        public class Customer_List
        {
            public int? Id { get; set; }
            public string? Text { get; set; }
            public string? Contact { get; set; }
        }
        public class Supplier_List
        {
            public int? Id { get; set; }
            public string? Text { get; set; }
            public string? Contact { get; set; }
        }
        public class Product_List
        {
            public int? Id { get; set; }
            public string? Text { get; set; }
            public string? AttIds { get; set; }
            public bool? IsExpiryApplied { get; set; }
            public decimal? UnitSalePrice { get; set; }
        }
        public class Product_Master_List
        {
            public Guid? GuID { get; set; }
            public string? Code { get; set; }
            public string? Description { get; set; }
            public string? Category { get; set; }
            public string? SubCategory { get; set; }
            public string? Brand { get; set; }
            public string? ProductType { get; set; }
            public int? DocumentStatus { get; set; }
        }
        public class SubCategory_Master_List
        {
            public Guid? GuID { get; set; }
            public string? Code { get; set; }
            public string? Description { get; set; }
            public string? Section { get; set; }
            public string? Category { get; set; }
            public int? DocumentStatus { get; set; }
        }
        public class Category_Master_List
        {
            public Guid? GuID { get; set; }
            public string? Code { get; set; }
            public string? Description { get; set; }
            public string? Section { get; set; }
            public int? DocumentStatus { get; set; }
        }
        public class Invoice_List
        {
            public string? CustomerName { get; set; }
            public string? Code { get; set; }
            public string? TransactionDate { get; set; }
            public decimal GrossAmount { get; set; }
            public decimal DiscountAmount { get; set; }
            public decimal TaxableAmount { get; set; }
            public decimal SaleTaxAmount { get; set; }
            public decimal AdditionalTaxAmount { get; set; }
            public decimal NetAmount { get; set; }
            public decimal DueAmount { get; set; }
            public Guid? GuID { get; set; }
            public int? InvoiceTypeId { get; set; }
            public int? InvoiceStatus { get; set; }
            public int? InvoiceId { get; set; }
        }
        public class Bill_List
        {
            public string? SupplierName { get; set; }
            public string? Code { get; set; }
            public string? TransactionDate { get; set; }
            public decimal GrossAmount { get; set; }
            public decimal DiscountAmount { get; set; }
            public decimal NetAmount { get; set; }
            public decimal DueAmount { get; set; }
            public Guid? GuID { get; set; }
            public int? BillTypeId { get; set; }
            public int? BillStatus { get; set; }
            public int? BillId { get; set; }
        }
        public class RptCustomerSummary_List
        {
            public int? CustomerId { get; set; }
            public string? Code { get; set; }
            public string? Description { get; set; }
            public string? Contact { get; set; }
            public decimal Receivable { get; set; }
            public decimal Receipt { get; set; }
            public decimal Due { get; set; }
        }
        public class RptSupplierSummary_List
        {
            public int? SupplierId { get; set; }
            public string? Code { get; set; }
            public string? Description { get; set; }
            public string? Contact { get; set; }
            public decimal Receivable { get; set; }
            public decimal Receipt { get; set; }
            public decimal Due { get; set; }
        }
        public class RptSaleLedger_List
        {
            public int? Customer { get; set; }
            public string? Code { get; set; }
            public DateTime? TransactionDate { get; set; }
            public string? Description { get; set; }
            public decimal? Debit { get; set; }
            public decimal Credit { get; set; }
            public int? DocumentType { get; set; }
            public Guid? GuID { get; set; }
            public int? CustomerId { get; set; }

        }
        public class RptInvoiceReceipt_List
        {
            public DateTime? TransactionDate { get; set; }
            public string? Code { get; set; }
            public string? Description { get; set; }
            public decimal? ReceiptAmount { get; set; }
            public Guid? GuID { get; set; }
            public int? Id { get; set; }

        }
        public class RptInventoryAdjustment_List
        {
            public DateTime? TransactionDate { get; set; }
            public string? Code { get; set; }
            public string? Description { get; set; }
            public decimal? QuantityIn { get; set; }
            public decimal? QuantityOut { get; set; }
            public decimal? UnitPurchasePrice { get; set; }
            public decimal? UnitSalePrice { get; set; }
            public Guid? GuID { get; set; }
            public int? Id { get; set; }

        }
        public class VMSRP_IProduct_CostingEngine
        {
            public int? ConsumedInventoryLedgerId { get; set; }
            public decimal QuantityOut { get; set; }
            public decimal UnitPurchasePrice { get; set; }
            public string? Batch { get; set; }
            public DateTime? ExpiryDate { get; set; }
            public bool IsStockDeficit { get; set; }
        }

        public class RptAFInvoiceHeader_List
        {
            public int InvoiceId { get; set; }
            public Guid GuID { get; set; }
            public string Code { get; set; }
            public string Location { get; set; }
            public DateTime? TransactionDate { get; set; }
            public string Customer { get; set; }
            public string Description { get; set; }
            public string FBRStamp { get; set; }
            public decimal DueAmount { get; set; }
            public int? InvoiceStatus { get; set; }
            public DateTime? CreatedOn { get; set; }
            public int? DocumentStatus { get; set; }
            public string UserName { get; set; }
            public decimal DocGrossAmount { get; set; }
            public decimal DocDiscountAmount { get; set; }
            public decimal DocTaxableAmount { get; set; }
            public decimal DocSaleTaxAmount { get; set; }
            public decimal DocAdditionalTaxAmount { get; set; }
            public decimal DocNetAmount { get; set; }
        }
        public class RptAFInvoiceDetail_List
        {
            public Guid GuID { get; set; }
            public string Description { get; set; }
            public string Attribute { get; set; }
            public decimal Quantity { get; set; }
            public decimal UnitSalePrice { get; set; }
            public decimal ActualAmount { get; set; }
            public decimal DiscountAmount { get; set; }
            public decimal ChargedAmount { get; set; }
        }



    }
}
