using Microsoft.EntityFrameworkCore;
using NuGet.ProjectModel;
using OrganisationSetup.Models.DAL;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.ViewModels;
using System;
using System.Linq;

namespace OrganisationSetup.Areas.AccountNfinance.Services
{
    public interface IAccountNfinanceRetriever
    {
        Task<List<AFChartOfAccount>> populateChartOfAccountByParam(string? operationType, int? filterConditionId, int? accountCatagoryId);
        Task<AFChartOfAccount> PopulateChartOfAccountInfo(Guid? guid);


    }
    public class AccountNfinanceRetrieverService : IAccountNfinanceRetriever
    {
        private readonly ERPOrganisationSetupContext _eRPOSContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountNfinanceRetrieverService(ERPOrganisationSetupContext eRPOSC, IHttpContextAccessor httpContextAccessor)
        {
            _eRPOSContext = eRPOSC;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task<List<AFChartOfAccount>> populateChartOfAccountByParam(string? operationType, int? filterConditionId, int? accountCatagoryId)
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);
            if (!userInfo.IsAuthenticated)
            {
                return new List<AFChartOfAccount>();
            }
            int?[]? documentStatusIds = operationType switch
            {
                nameof(OperationType.INSERT_DATA_INTO_DB) => [(int?)DocumentStatus.active],
                nameof(OperationType.UPDATE_DATA_INTO_DB) => [(int?)DocumentStatus.active, (int?)DocumentStatus.inactive, (int?)DocumentStatus.deleted],
                _ => null
            };
            if (documentStatusIds == null) return new List<AFChartOfAccount>();
            List<AFChartOfAccount> BranchRecord = new List<AFChartOfAccount>();
            switch (filterConditionId)
            {
                case ((int?)FilterConditions.afChartOfAccount_Operation_ByCompanyId):
                    return await _eRPOSContext.AFChartOfAccount.AsNoTracking()
                        .Where(x =>
                        x.CompanyId == userInfo.CompanyId
                        && x.Status == true
                        && documentStatusIds.Contains(x.DocumentStatus)).Select(x => new AFChartOfAccount
                        {
                            Id = x.Id,
                            GuID = x.GuID,
                            Description = x.Description
                        }).ToListAsync();
                default:
                    return new List<AFChartOfAccount>();
            }
        }
        public async Task<AFChartOfAccount> PopulateChartOfAccountInfo(Guid? guid)
        {
            if (!guid.HasValue)
            {
                return new AFChartOfAccount();
            }

            return await _eRPOSContext.AFChartOfAccount
                .AsNoTracking()
                .Where(x =>
                    x.GuID == guid.Value &&
                    x.Status == true 
                    ).Select(x => new AFChartOfAccount
                {
                    Id = x.Id,
                    GuID = x.GuID,
                    Description = x.Description
                }).FirstOrDefaultAsync() ?? new AFChartOfAccount();
        }

    }
}
