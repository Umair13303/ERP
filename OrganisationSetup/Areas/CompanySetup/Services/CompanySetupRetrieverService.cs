using Microsoft.EntityFrameworkCore;
using NuGet.ProjectModel;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Services;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.ViewModels;
using System;
using System.Linq;

namespace OrganisationSetup.Areas.CompanySetup.Services
{
    public interface ICompanySetupRetriever
    {
        Task<List<CSDepartment>> populateDepartmentByParam(string? operationType, int? filterConditionId);
    }
    public class CompanySetupRetriever : ICompanySetupRetriever
    {
        private readonly TempUser _currentUser;
        private readonly ERPOrganisationSetupContext _eRPOSContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICommon _commonsServices;


        public CompanySetupRetriever(TempUser currentUser,ERPOrganisationSetupContext eRPOSC, IHttpContextAccessor httpContextAccessor, ICommon commonsServices)
        {
            _currentUser = currentUser;
            _eRPOSContext = eRPOSC;
            _httpContextAccessor = httpContextAccessor;
            _commonsServices = commonsServices;

        }
        public async Task<List<CSDepartment>> populateDepartmentByParam(string? operationType, int? filterConditionId)
        {
            var userInfo = _currentUser;
            if (!userInfo.IsAuthenticated)
            {
                return new List<CSDepartment>();
            }

            int?[]? documentStatusIds = await _commonsServices.getDocumentStatusByParam(operationType);
            if (documentStatusIds == null) return new List<CSDepartment>();
            switch (filterConditionId)
            {
                case ((int?)FilterConditions.CSDepartment_Operation_ByCompany):
                    return await _eRPOSContext.CSDepartment.AsNoTracking()
                        .Where(x =>
                        x.CompanyId == userInfo.CompanyId
                        && x.Status == true
                        && documentStatusIds.Contains(x.DocumentStatus)).Select(x => new CSDepartment
                        {
                            Id = x.Id,
                            GuID = x.GuID,
                            Description = x.Description
                        }).ToListAsync();
                case ((int?)FilterConditions.CSDepartment_Operation_ForSale):
                    return await _eRPOSContext.CSDepartment.AsNoTracking()
                        .Where(d => _eRPOSContext.ICategory.Any(c => c.DepartmentId == d.Id)
                        && d.CompanyId == userInfo.CompanyId
                        && d.Status == true
                        && documentStatusIds.Contains(d.DocumentStatus)).Select(x => new CSDepartment
                        {
                            Id = x.Id,
                            GuID = x.GuID,
                            Description = x.Description
                        }).ToListAsync();
                default:
                    return new List<CSDepartment>();
            }
        }
    }

}
