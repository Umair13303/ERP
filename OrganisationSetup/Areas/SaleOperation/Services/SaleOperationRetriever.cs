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
using static SharedUI.Models.ViewModels.DTObject;

namespace OrganisationSetup.Areas.SaleOperation.Services
{
    public interface ISaleOperationRetriever
    {
        Task<List<Customer_List>> populateCustomerByParam(string? operationType, int? filterConditionId);
        Task<IEnumerable<DTObject.Customer_List_Spec>> populateCustomerSummByParam(string operationType);
    }
    public class SaleOperationRetrieverService : ISaleOperationRetriever
    {
        private readonly TempUser _currentUser;
        private readonly ERPOrganisationSetupContext _eRPOSContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICommon _commonsServices;
        private readonly IOSDataLayer _repo;
        private readonly string _connectionString;


        public SaleOperationRetrieverService(TempUser currentUser, ERPOrganisationSetupContext eRPOSC, IHttpContextAccessor httpContextAccessor, ICommon commonsServices, IOSDataLayer repo)
        {
            _currentUser = currentUser;
            _eRPOSContext = eRPOSC;
            _httpContextAccessor = httpContextAccessor;
            _commonsServices = commonsServices;
            _repo = repo;
            _connectionString = _eRPOSContext.Database.GetDbConnection().ConnectionString;
        }
        public async Task<List<Customer_List>> populateCustomerByParam(string? operationType, int? filterConditionId)
        {
            var userInfo = _currentUser;
            if (!userInfo.IsAuthenticated)
            {
                return new List<Customer_List>();
            }

            int?[]? documentStatusIds = await _commonsServices.getDocumentStatusByParam(operationType);
            if (documentStatusIds == null) return new List<Customer_List>();
            switch (filterConditionId)
            {
                case ((int?)FilterConditions.SOCustomer_Operation_ByCompany):
                    return await _eRPOSContext.SOCustomer.AsNoTracking()
                        .Where(x =>
                        x.CompanyId == userInfo.CompanyId
                        && x.BranchId == userInfo.BranchId
                        && x.Status == true
                        && documentStatusIds.Contains(x.DocumentStatus)).Select(x => new Customer_List
                        {
                            Id = x.Id,
                            Description = x.Description,
                            Contact = x.Contact,
                        }).ToListAsync();
                default:
                    return new List<Customer_List>();
            }
        }
        public async Task<IEnumerable<DTObject.Customer_List_Spec>> populateCustomerSummByParam(string operationType)
        {
            var userInfo = _currentUser;
            if (!userInfo.IsAuthenticated) return new List<DTObject.Customer_List_Spec>();
            int?[]? paymentStatusIds = await _commonsServices.getPaymentStatusByParam();
            int?[]? invoiceStatusIds = await _commonsServices.getInvoiceStatusByParam();
            int?[]? documentStatusIds = await _commonsServices.getDocumentStatusByParam(operationType);

            return await _repo.ret_Customer_ByParam(
                userInfo.BranchId,
                userInfo.CompanyId,
                paymentStatusIds,
                invoiceStatusIds,
                documentStatusIds,
                _connectionString
            );
        }
    }

}
