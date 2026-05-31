using Microsoft.EntityFrameworkCore;
using NuGet.ProjectModel;
using OrganisationSetup.Models.DAL;
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
    }

    public class ProcurementRetrieverService : IProcurementRetriever
    {
        private readonly TempUser _currentUser;
        private readonly ERPOrganisationSetupContext _eRPOSContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICommon _commonsServices;


        public ProcurementRetrieverService(TempUser currentUser,ERPOrganisationSetupContext eRPOSC, IHttpContextAccessor httpContextAccessor, ICommon commonsServices)
        {
            _currentUser = currentUser;
            _eRPOSContext = eRPOSC;
            _httpContextAccessor = httpContextAccessor;
            _commonsServices = commonsServices;
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
    }
}