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
        Task<IEnumerable<DTObject.Invoice_List>> populateInvoiceByParam(string operationType, Guid? guid, int? customerId, int?[]invoiceStatus);
        Task<IEnumerable<DTObject.RptInvoiceReceipt_List>> populateInvoiceReceiptByParam(string operationType, int? customerId);
        Task<IEnumerable<DTObject.Bill_List>> populateBillByParam(string operationType, Guid? guId, int? supplierId, int?[] billStatus);
    }
    public class AccountNfinanceRetrieverService : IAccountNfinanceRetriever
    {
        private readonly ERPOrganisationSetupContext _eRPOSContext;
        private readonly TempUser _currentUser;
        private readonly ICommon _commonsServices;
        private readonly IOSDataLayer _repo;
        private readonly string _connectionString;


        public AccountNfinanceRetrieverService(TempUser currentUser,ERPOrganisationSetupContext eRPOSC,ICommon commonsServices, IOSDataLayer repo)
        {
            _currentUser = currentUser;
            _eRPOSContext = eRPOSC;
            _commonsServices = commonsServices;
            _repo = repo;
            _connectionString = _eRPOSContext.Database.GetDbConnection().ConnectionString;

        }
        public async Task<List<AFChartOfAccount>> populateChartOfAccountByParam(string? operationType, int? filterConditionId, int? accountCatagoryId)
        {
            var userInfo = _currentUser;
            if (!userInfo.IsAuthenticated)
            {
                return new List<AFChartOfAccount>();
            }
            int?[]? documentStatusIds = await _commonsServices.getDocumentStatusByParam(operationType);
            if (documentStatusIds == null) return new List<AFChartOfAccount>();
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
        
        public async Task<IEnumerable<DTObject.Invoice_List>> populateInvoiceByParam(string operationType, Guid? guId, int? customerId, int?[] invoiceStatus)
        {
            var userInfo = _currentUser;
            if (!userInfo.IsAuthenticated) return new List<DTObject.Invoice_List>();
            int?[]? documentStatusIds = await _commonsServices.getDocumentStatusByParam(operationType);

            return await _repo.ret_Invoice_ByParam(
                guId,
                customerId,
                documentStatusIds,
                invoiceStatus,
                _connectionString
            );
        }
        public async Task<IEnumerable<DTObject.RptInvoiceReceipt_List>> populateInvoiceReceiptByParam(string operationType, int? customerId)
        {
            var userInfo = _currentUser;
            if (!userInfo.IsAuthenticated) return new List<DTObject.RptInvoiceReceipt_List>();
            int?[]? paymentStatusIds = await _commonsServices.getPaymentStatusByParam();
            int?[]? documentStatusIds = await _commonsServices.getDocumentStatusByParam(operationType);

            return await _repo.ret_RptInvoiceReceipt_ByParam(
                paymentStatusIds,
                documentStatusIds,
                userInfo.BranchId,
                userInfo.CompanyId,
                customerId,
                _connectionString
            );
        }
        public async Task<IEnumerable<DTObject.Bill_List>> populateBillByParam(string operationType, Guid? guId, int? supplierId, int?[] billStatus)
        {
            var userInfo = _currentUser;
            if (!userInfo.IsAuthenticated) return new List<DTObject.Bill_List>();
            int?[]? documentStatusIds = await _commonsServices.getDocumentStatusByParam(operationType);

            return await _repo.ret_Bill_ByParam(
                guId,
                supplierId,
                documentStatusIds,
                billStatus,
                _connectionString
            );
        }
    }
}
