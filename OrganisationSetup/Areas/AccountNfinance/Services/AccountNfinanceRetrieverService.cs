using Microsoft.EntityFrameworkCore;
using NuGet.ProjectModel;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Models.DAL.StoredProcedure;
using OrganisationSetup.Services;
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
        Task<AFChartOfAccount> populateChartOfAccountInfo(Guid? guid);
        Task<IEnumerable<DTObject.Invoice_List>> populateInvoiceListByParam(string operationType, Guid? guid, int customerId);
    }
    public class AccountNfinanceRetrieverService : IAccountNfinanceRetriever
    {
        private readonly ERPOrganisationSetupContext _eRPOSContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TempUser _currentUser;
        private readonly ICommon _commonsServices;
        private readonly IDapper _dapper;


        public AccountNfinanceRetrieverService(TempUser currentUser,ERPOrganisationSetupContext eRPOSC, IHttpContextAccessor httpContextAccessor, ICommon commonsServices, IDapper dapper)
        {
            _currentUser = currentUser;
            _eRPOSContext = eRPOSC;
            _httpContextAccessor = httpContextAccessor;
            _commonsServices = commonsServices;
            _dapper = dapper;
        }
        public async Task<List<AFChartOfAccount>> populateChartOfAccountByParam(string? operationType, int? filterConditionId, int? accountCatagoryId)
        {
            var userInfo = _currentUser;
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
        public async Task<AFChartOfAccount> populateChartOfAccountInfo(Guid? guid)
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
        
        public async Task<IEnumerable<DTObject.Invoice_List>> populateInvoiceListByParam(string operationType, Guid? guid,int customerId)
        {
            var userInfo = _currentUser;
            if (!userInfo.IsAuthenticated) return new List<DTObject.Invoice_List>();
            int?[]? documentStatusIds = await _commonsServices.getDocumentStatusByParam(operationType);
            int?[]? invoiceStatusIds = await _commonsServices.getDocumentStatusByParam(operationType);


            return await _dapper.populateInvoiceListByParam(
                guid,
                operationType,
                documentStatusIds,
                invoiceStatusIds,
                customerId
            );
        }
    }
}
