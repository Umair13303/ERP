using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Models.DAL.StoredProcedure;
using SharedUI.Models.Configurations;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.ViewModels;

namespace OrganisationSetup.Services
{
    public interface ICommon
    {
        Task<int?[]?> getDocumentStatusByParam(string? operationType); 
        Task<int?[]?> getPaymentStatusByParam();
        Task<int?[]?> getInvoiceStatusByParam();
        Task<List<vOrganisationType>> populateOrganisationTypeByParam();
        Task<List<vCountry>> populateCountryByParam();
        Task<List<vCity>> populateCityByParam(int? countryId);
        Task<List<vRole>> populateRoleByParam();
        Task<List<vAccountType>> populateAccountTypeByParam();
        Task<List<vAccountCatagory>> populateAccountCatagoryByParam(int? accountTypeId);
        Task<List<vFinancialStatement>> populateFinancialStatementByParam();
        Task<List<vAttribute>> populateAttributeByParam();
        Task<List<vItemType>> populateItemTypeByParam();
        Task<List<vHSCode>> populateHSCodeByParam();
        Task<List<vSaleTaxType>> populateSaleTaxTypeByParam();
        Task<List<osvChartOfAccount>> populateOSvChartOfAccountByParam(string? operationType, int? filterConditionId, int? accountCatagoryId);
        Task<List<vPaymentMethod>> populatePaymentMethodByParam();
        Task<List<vProductType>> populateProductTypeByParam();
        Task<Dictionary<string, FieldConfig>> fetchProductSetting();
    }
    public class CommonServices : ICommon
    {
        private readonly TempUser _currentUser;
        private readonly ERPOrganisationSetupContext _context;
        private readonly IConfiguration _conf;


        public CommonServices(TempUser currentUser, ERPOrganisationSetupContext context, IConfiguration conf)
        {
            _currentUser = currentUser;
            _context = context;
            _conf = conf;
        }
        public async Task<int?[]?> getDocumentStatusByParam(string? operationType)
        {
            int?[]? documentStatusIds = operationType switch
            {
                nameof(OperationType.INSERT_DATA_INTO_DB) => [(int?)DocumentStatus.active],
                nameof(OperationType.MPO_LIST) => [(int?)DocumentStatus.active],
                nameof(OperationType.UPDATE_DATA_INTO_DB) => [(int?)DocumentStatus.active, (int?)DocumentStatus.inactive, (int?)DocumentStatus.deleted],
                _ => null
            };
            return await Task.FromResult(documentStatusIds);
        }
        public async Task<int?[]?> getPaymentStatusByParam()
        {
            int?[]? paymentStatusIds = [(int?)PaymentStatus.declined, (int?)PaymentStatus.verified, (int?)PaymentStatus.underProcess];
            return await Task.FromResult(paymentStatusIds);
        }
        public async Task<int?[]?> getInvoiceStatusByParam()
        {
            int?[]? invoiceStatusIds = [(int?)InvoiceStatus.unPaid, (int?)InvoiceStatus.partialPaid, (int?)InvoiceStatus.paid, (int?)InvoiceStatus.overDue, (int?)InvoiceStatus.cancelled];
            return await Task.FromResult(invoiceStatusIds);
        }
        public async Task<List<vOrganisationType>> populateOrganisationTypeByParam()
        {
            var result = await _context.vOrganisationType.AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<List<vCountry>> populateCountryByParam()
        {
            var result = await _context.vCountry.AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<List<vCity>> populateCityByParam(int? countryId)
        {
            var result = await _context.vCity.AsNoTracking().Where(x=> x.CountryId == countryId).ToListAsync();
            return result;
        }
        public async Task<List<vRole>> populateRoleByParam()
        {
            var result = await _context.vRole.AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<List<vAccountType>> populateAccountTypeByParam()
        {
            var result = await _context.vAccountType.AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<List<vAccountCatagory>> populateAccountCatagoryByParam(int? accountTypeId)
        {
            var result = await _context.vAccountCatagory.AsNoTracking().Where(x=> x.AccountTypeId == accountTypeId).ToListAsync();
            return result;
        }
        public async Task<List<vFinancialStatement>> populateFinancialStatementByParam()
        {
            var result = await _context.vFinancialStatement.AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<List<vAttribute>> populateAttributeByParam()
        {
            var result = await _context.vAttribute.AsNoTracking().Where(x => x.Status == true).ToListAsync();
            return result;
        }
        public async Task<List<vItemType>> populateItemTypeByParam()
        {
            var result = await _context.vItemType.AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<List<vHSCode>> populateHSCodeByParam()
        {
            var result = await _context.vHSCode.AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<List<vSaleTaxType>> populateSaleTaxTypeByParam()
        {
            var result = await _context.vSaleTaxType.AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<List<vPaymentMethod>> populatePaymentMethodByParam()
        {
            var result = await _context.vPaymentMethod.AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<List<vProductType>> populateProductTypeByParam()
        {
            var result = await _context.vProductType.AsNoTracking().Where(x=> x.Status == true).ToListAsync();
            return result;
        }
        public async Task<List<osvChartOfAccount>> populateOSvChartOfAccountByParam(string? operationType, int? filterConditionId, int? accountCatagoryId)
        {
            var userInfo = _currentUser;
            if (!userInfo.IsAuthenticated)
            {
                return new List<osvChartOfAccount>();
            }
            int?[]? documentStatusIds = await getDocumentStatusByParam(operationType);
            if (documentStatusIds == null) return new List<osvChartOfAccount>();
            List<osvChartOfAccount> accountRecord = new List<osvChartOfAccount>();
            switch (filterConditionId)
            {
                case ((int?)FilterConditions.osvChartOfAccount_Operation_ByDefaultSetting):
                    return await _context.osvChartOfAccount.AsNoTracking()
                        .Where(x =>
                        x.CompanyId == userInfo.CompanyId
                        && x.AccountCategoryId == accountCatagoryId
                        && x.Status == true
                        && documentStatusIds.Contains(x.DocumentStatus)).Select(x => new osvChartOfAccount
                        {
                            Id = x.Id,
                            GuID = x.GuID,
                            Description = x.Description
                        }).ToListAsync();
                default:
                    return new List<osvChartOfAccount>();
            }
        }
        public async Task<Dictionary<string, FieldConfig>> fetchProductSetting()
        {
            var clientKEY = _conf.GetValue<int>("ClientKEY");
            var settingList = await _context.confClientProductSetting.AsNoTracking().Where(x => x.Status == true && x.ClientKEY == clientKEY).FirstOrDefaultAsync();

            var result = new Dictionary<string, FieldConfig>
            {
                { nameof(ProductConfig.MachineNumberConf), new FieldConfig {
                    Display = (settingList.EnableMachineNumber ?? false) ? "block" : "none",
                    DefaultValue = Guid.NewGuid().ToString().Replace("-","")
                }},
                { nameof(ProductConfig.SKUConf), new FieldConfig {
                    Display = (settingList.EnableSKU ?? false) ? "block" : "none",
                    DefaultValue = Guid.NewGuid().ToString().Replace("-","")
                }},

                { nameof(ProductConfig.AttributeConf), new FieldConfig {
                    Display = (settingList.EnableAttribute ?? false) ? "block" : "none",
                    DefaultValue = ""
                }},
                { nameof(ProductConfig.FavoriteConf), new FieldConfig {
                    Display = (settingList.EnableFavorite ?? false) ? "block" : "none",
                    DefaultValue = false
                }},
                { nameof(ProductConfig.SaleTaxConf), new FieldConfig {
                    Display = (settingList.EnableTaxSetting ?? false) ? "block" : "none",
                    DefaultValue = false
                }},
                { nameof(ProductConfig.ExpiryConf), new FieldConfig {
                    Display = (settingList.EnableExpiry ?? false) ? "block" : "none",
                    DefaultValue = false
                }},
                { nameof(ProductConfig.ATIConf), new FieldConfig {
                    Display = (settingList.EnableATI ?? false) ? "block" : "none",
                    DefaultValue = ""
                }},
                { nameof(ProductConfig.ProductTypeConf), new FieldConfig {
                    Label = settingList.ProductTypeLabel ?? "Product Type"
                }},
                { nameof(ProductConfig.DepartmentConfig), new FieldConfig {
                    Display = (settingList.EnableDepartment ?? false) ? "block" : "none",
                    DefaultValue = ""
                }},
            }; 
            return result;
        }

    }
}
