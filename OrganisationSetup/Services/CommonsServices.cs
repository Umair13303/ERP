using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Models.DAL.StoredProcedure;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.ViewModels;

namespace OrganisationSetup.Services
{
    public interface ICommon
    {
        Task<int?[]?> getDocumentStatusByParam(string? operationType);
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

    }
    public class CommonServices : ICommon
    {
        private readonly ERPOrganisationSetupContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CommonServices( ERPOrganisationSetupContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int?[]?> getDocumentStatusByParam(string? operationType)
        {
            int?[]? documentStatusIds = operationType switch
            {
                nameof(OperationType.INSERT_DATA_INTO_DB) => [(int?)DocumentStatus.active],
                nameof(OperationType.UPDATE_DATA_INTO_DB) => [(int?)DocumentStatus.active, (int?)DocumentStatus.inactive, (int?)DocumentStatus.deleted],
                _ => null
            };
            return await Task.FromResult(documentStatusIds);
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
            var result = await _context.vAttribute.AsNoTracking().ToListAsync();
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
        public async Task<List<osvChartOfAccount>> populateOSvChartOfAccountByParam(string? operationType, int? filterConditionId, int? accountCatagoryId)
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);
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

    }
}
