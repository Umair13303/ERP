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

namespace OrganisationSetup.Areas.Procurement.Services
{
    public interface IProcurementRetriever
    {
        Task<List<Supplier_List>> populateSupplierByParam(string? operationType, int? filterConditionId);
        Task<IEnumerable<DTObject.RptSupplierSummary_List>> populateSupplierSummByParam(string operationType, int?[]? supplierId);

    }

    public class ProcurementRetrieverService : IProcurementRetriever
    {
        private readonly TempUser _currentUser;
        private readonly ERPOrganisationSetupContext _eRPOSContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICommon _commonsServices;
        private readonly IOSDataLayer _repo;
        private readonly string _connectionString;


        public ProcurementRetrieverService(IOSDataLayer repo,TempUser currentUser,ERPOrganisationSetupContext eRPOSC, IHttpContextAccessor httpContextAccessor, ICommon commonsServices)
        {
            _currentUser = currentUser;
            _eRPOSContext = eRPOSC;
            _httpContextAccessor = httpContextAccessor;
            _commonsServices = commonsServices;
            _repo = repo;
            _connectionString = _eRPOSContext.Database.GetDbConnection().ConnectionString;

        }
        public async Task<List<Supplier_List>> populateSupplierByParam(string? operationType, int? filterConditionId)
        {
            var userInfo = _currentUser;
            if (!userInfo.IsAuthenticated)
            {
                return new List<Supplier_List>();
            }

            int?[]? documentStatusIds = await _commonsServices.getDocumentStatusByParam(operationType);
            if (documentStatusIds == null) return new List<Supplier_List>();
            switch (filterConditionId)
            {
                case ((int?)FilterConditions.PSupplier_Operation_ByCompany):
                    return await _eRPOSContext.PSupplier.AsNoTracking()
                        .Where(x =>
                        x.CompanyId == userInfo.CompanyId
                        && x.BranchId == userInfo.BranchId
                        && x.Status == true
                        && documentStatusIds.Contains(x.DocumentStatus)).Select(x => new Supplier_List
                        {
                            Id = x.Id,
                            Text = x.Description,
                            Contact = x.Contact,
                        }).ToListAsync();
                default:
                    return new List<Supplier_List>();
            }
        }

        public async Task<IEnumerable<DTObject.RptSupplierSummary_List>> populateSupplierSummByParam(string operationType, int?[]? supplierIds)
        {
            var userInfo = _currentUser;
            if (!userInfo.IsAuthenticated) return new List<DTObject.RptSupplierSummary_List>();
            int?[]? paymentStatusIds = await _commonsServices.getPaymentStatusByParam();
            int?[]? invoiceStatusIds = await _commonsServices.getInvoiceStatusByParam();
            int?[]? documentStatusIds = await _commonsServices.getDocumentStatusByParam(operationType);

            return await _repo.ret_RptSupplierSummary_ByParam(
                userInfo.BranchId,
                userInfo.CompanyId,
                paymentStatusIds,
                invoiceStatusIds,
                documentStatusIds,
                supplierIds,
                _connectionString
            );
        }
    }
}