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
            public Guid? GuID { get; set; }
            public string? CustomerName { get; set; }
            public string? Code { get; set; }
            public string? TransactionDate { get; set; }
            public decimal GrossAmount { get; set; }
            public decimal DiscountAmount { get; set; }
            public decimal NetAmount { get; set; }
            public decimal SalesTaxAmount { get; set; }
            public decimal AdditionalTaxAmount { get; set; }
            public decimal DueAmount { get; set; }
            public int? InvoiceTypeId { get; set; }
            public int? InvoiceStatus { get; set; }

        }

    }
}
