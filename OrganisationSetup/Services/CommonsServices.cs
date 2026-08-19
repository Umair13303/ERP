using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Models.DAL.StoredProcedure;
using SharedUI.Models.Configurations;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.TVP;
using SharedUI.Models.ViewModels;
using System.Linq;
using static SharedUI.Models.Enums.SetupRoute;

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
        Task<List<vInventoryAdjustmentType>> populateInventoryAdjustmentTypeByParam();
        Task<List<vCostingMode>> populateCostingModeByParam();
        Task<List<vTierType>> populateTierTypeByParam();
        Task<Dictionary<string, FieldConfig>> fetchProductSetting();
        Task<int> generate_productCombination(int? refDocumentType, List<IProductCCE> combinationEngine);
        Task<int> get_productCombination(int? productId, string attributeKey);
        Task<object> get_productPricingbyParam(int? productId, int? productCombinationId, int? locationId, int? tierTypeId);
        Task<Dictionary<int, int>> get_ActiveATIByParam(IEnumerable<int> productIds);
        Task<confApplicationRule> get_configurationRuleByClientSetting();

        //Task iProductCCE_SPR(int refDocumentType, List<IInventoryAdjustmentPPQD_TVP> lines, bool allowCreate);
    }
    public class CommonServices : ICommon
    {
        private readonly TempUser _currentUser;
        private readonly ERPOrganisationSetupContext _context;
        private readonly IConfiguration _conf;

        public async Task<object> isCompanyConfigured()
        {
            #region REGION TO VALIDATE COA SEEDING
            bool isDefaulAccountSeeded = false;
            var seederCOA = await _context.osvChartOfAccount.Select(x=> x.GuID).ToListAsync();
            var existingCOA = await _context.AFChartOfAccount.Where(x => seederCOA.Contains(x.GuID)).AnyAsync();
            isDefaulAccountSeeded = !existingCOA;
            #endregion

            return isDefaulAccountSeeded;
        }
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
            var result = await _context.vCity.AsNoTracking().Where(x => x.CountryId == countryId).ToListAsync();
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
            var result = await _context.vAccountCatagory.AsNoTracking().Where(x => x.AccountTypeId == accountTypeId).ToListAsync();
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
            var result = await _context.vProductType.AsNoTracking().Where(x => x.Status == true).ToListAsync();
            return result;
        }
        public async Task<List<vInventoryAdjustmentType>> populateInventoryAdjustmentTypeByParam()
        {
            var result = await _context.vInventoryAdjustmentType.AsNoTracking().Where(x => x.Status == true).ToListAsync();
            return result;
        }
        public async Task<List<vCostingMode>> populateCostingModeByParam()
        {
            var result = await _context.vCostingMode.AsNoTracking().Where(x => x.Status == true).ToListAsync();
            return result;
        }
        public async Task<List<vTierType>> populateTierTypeByParam()
        {
            var result = await _context.vTierType.AsNoTracking().Where(x => x.Status == true && x.IsDefault == true).ToListAsync();
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
            var settingList = await _context.confclientproductsetting.AsNoTracking().Where(x => x.Status == true && x.ClientKEY == clientKEY).FirstOrDefaultAsync();

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
        public async Task<int> generate_productCombination(int? refDocumentType, List<IProductCCE> combinationList)
        {
            if (combinationList == null || !combinationList.Any())
            {
                return 400;
            }
            foreach (var item in combinationList)
            {
                var isExist = await _context.IProductCCE.Where(x => x.ProductId == item.ProductId && x.Description.Trim().ToLower() == item.Description.Trim().ToLower()).AnyAsync();
                if (isExist == false)
                {
                    var combination = new IProductCCE
                    {
                        GuID = Guid.NewGuid(),
                        RefDocumentType = refDocumentType,
                        ProductId = item.ProductId,
                        Description = item.Description,
                        QRCode = item.QRCode,
                        CreatedOn = DateTime.Now,
                        CreatedBy = _currentUser.UserId,
                        DocumentType = (int)DocumentType.productCombination,
                        DocumentStatus = (int)DocumentStatus.active,
                        Status = true
                    };
                    _context.IProductCCE.Add(combination);
                }
            }
            await _context.SaveChangesAsync();
            return 200;
        }
        public async Task<int> get_productCombination(int? productId, string description)
        {
            int productCombinationId = await _context.IProductCCE.Where(x => x.ProductId == productId && x.Description == description).Select(x => x.Id).FirstOrDefaultAsync();
            return productCombinationId;
        }
        public async Task<object> get_productPricingbyParam(int? productId, int? productCombinationId, int? locationId, int? tierTypeId)
        {
            var result = await _context.AFProductPriceLog
                .Where(pl => pl.ProductId == productId
                    && (productCombinationId == null || pl.ProductCombinationId == productCombinationId)
                    && pl.Status == true
                    && pl.DocumentStatus == (int)DocumentStatus.active
                    && pl.CompanyId == _currentUser.CompanyId
                    && (locationId == 0 || pl.BranchId == locationId)
                    && (tierTypeId == 0 || pl.TierTypeId == tierTypeId))
                .OrderByDescending(pl => pl.CreatedOn)
                .Select(x => new
                {
                    ProductPriceLogId = x.Id,
                    UnitSalePrice = x.DefaultSalePrice,
                    x.MinimumSalePrice
                })
                .FirstOrDefaultAsync();

            return result;
        }
        public async Task<Dictionary<int, int>> get_ActiveATIByParam(IEnumerable<int> productIds)
        {
            var validIds = productIds.Where(id => id > 0).Distinct().ToList();

            if (!validIds.Any())
            {
                return new Dictionary<int, int>();
            }

            var rawAtiList = await _context.IProductATI
                .AsNoTracking()
                .Where(x => validIds.Contains((int)x.ProductId)
                         && x.DocumentStatus == (int)DocumentStatus.active
                         && x.Status == true)
                .Select(x => new { x.ProductId, x.Id, x.CreatedOn })
                .ToListAsync();

            return rawAtiList
                .GroupBy(x => x.ProductId.Value)
                .ToDictionary(
                    g => g.Key,
                    g => g.OrderByDescending(x => x.CreatedOn).First().Id
                );
        }

        public async Task<confApplicationRule> get_configurationRuleByClientSetting()
        {
            int clientKEY = _conf.GetValue<int>("ClientKEY");

            var data = await _context.confApplicationRule.FirstOrDefaultAsync(x => x.ClientKEY == clientKEY);

            return data;
        }
        
        private async Task iProductCCE_SPR(int refDocumentType, List<IInventoryAdjustmentPPQD_TVP> invADJ_items)
        {
            List<int?> productIds;
            List<IProductCCE> existingCombination;
            List<IProductCCE> newCombination = new List<IProductCCE>();

            switch (refDocumentType)
            {
                case (int)DocumentType.inventoryAdjustment:
                    productIds = invADJ_items.Select(x => x.ProductId).Distinct().ToList();
                    existingCombination = await _context.IProductCCE.Where(x => productIds.Contains(x.ProductId)).ToListAsync();
                    foreach (var item in invADJ_items)
                    {
                        var formattedDescription = item.Attribute?.Trim().ToLower();
                        var productCombination = existingCombination.FirstOrDefault(x => x.ProductId == item.ProductId && x.Description?.Trim().ToLower() == formattedDescription) ?? newCombination.FirstOrDefault(x => x.ProductId == item.ProductId && x.Description?.Trim().ToLower() == formattedDescription);
                        if (productCombination == null)
                        {
                            var combination = new IProductCCE
                            {
                                GuID = Guid.NewGuid(),
                                RefDocumentType = refDocumentType,
                                ProductId = item.ProductId,
                                Description = item.Attribute,
                                CreatedOn = DateTime.UtcNow,
                                CreatedBy = _currentUser.UserId,
                                DocumentType = (int)DocumentType.productCombination,
                                DocumentStatus = (int)DocumentStatus.active,
                                Status = true
                            };
                            newCombination.Add(combination);
                            _context.IProductCCE.Add(combination);
                        }
                    }
                    if (newCombination.Any())
                    {
                        await _context.SaveChangesAsync();
                        existingCombination.AddRange(newCombination);
                    }
                    foreach (var item in invADJ_items)
                    {
                        var cleanDesc = item.Attribute?.Trim().ToLower();
                        item.ProductCombinationId = existingCombination.FirstOrDefault(x => x.ProductId == item.ProductId && x.Description?.Trim().ToLower() == cleanDesc).Id;
                    }
                    break;
            }
        }

    }

}
