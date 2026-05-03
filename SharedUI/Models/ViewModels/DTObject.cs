using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            public string? Description { get; set; }
            public string? Contact { get; set; }
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
    }
}
