using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUI.Models.SQLParameters
{
    public class PostedData
    {
        public string? OperationType { get; set; }
        public Guid? GuID { get; set; }
        public Guid? ProductATIGuID { get; set; }
        public string? Description { get; set; }
        public string? DefaultReceivableAccount { get; set; }
        public string? FBRStamp { get; set; }
        public int? OrganisationTypeId { get; set; }
        public int? CountryId { get; set; }
        public int? CustomerId { get; set; }
        public int? AccountTypeId { get; set; }
        public int? AccountCategoryId { get; set; }
        public int? FinancialStatementId { get; set; }
        public int? CityId { get; set; }
        public int? LocationId { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public string? Contact { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Website { get; set; }
        public string? Logo { get; set; }
        public string? NTNNumber { get; set; }
        public string? Password { get; set; }
        public string? MachineNumber { get; set; }
        public string? SKU { get; set; }
        public string? AllowedBranchIds { get; set; }
        public string? AdditionalDetail { get; set; }
        public string? AttributeIds { get; set; }
        public string? CNICNumber { get; set; }
        public decimal CriticalLimit { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public decimal DueAmount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DocType { get; set; }
        public int? InvoiceTypeId { get; set; }
        public int? DocumentStatus { get; set; }
        public int? InvoiceStatus { get; set; }
        public bool? IsFavorite { get; set; }
        public bool? IsSaleTaxExclusive { get; set; }
        public bool? Status { get; set; }
        public int? BranchId { get; set; }
        public int? BrandId { get; set; }
        public int? CompanyId { get; set; }
        public int? EmployeeId { get; set; }
        public int? RoleId { get; set; }
        public int? ProductId { get; set; }
        public int? InventoryAccountId { get; set; }
        public int? SaleRevenueAccountId { get; set; }
        public int? CostOfSaleAccountId { get; set; }
        public int? ReceivableAccountId { get; set; }
        public int? SaleUnitId { get; set; }
        public int? ItemTypeId { get; set; }
        public int? HSCodeId { get; set; }
        public int? SaleTaxTypeId { get; set; }
    }
}
