using Microsoft.EntityFrameworkCore;
using NuGet.ProjectModel;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Services;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.ViewModels;
using System;
using System.Linq;

namespace OrganisationSetup.Areas.ApplicationConfiguration.Services
{
    public interface IApplicationConfigurationRetriever
    {
        Task<List<ACCompany>> populateCompanyByParam(string? operationType, int? filterConditionId);
        Task<List<ACBranch>> populateBranchByParam(string? operationType, int? filterConditionId, int? companyId);
        Task<List<ACSaleUnit>> populateSaleUnitByParam(string? operationType, int? filterConditionId);
        }
    public class ApplicationConfigurationRetrieverService : IApplicationConfigurationRetriever
    {
        private readonly ERPOrganisationSetupContext _eRPOSContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICommon _commonsServices;


        public ApplicationConfigurationRetrieverService(ERPOrganisationSetupContext eRPOSC, IHttpContextAccessor httpContextAccessor, ICommon commonsServices)
        {
            _eRPOSContext = eRPOSC;
            _httpContextAccessor = httpContextAccessor;
            _commonsServices = commonsServices;

        }
        public async Task<List<ACCompany>> populateCompanyByParam(string? operationType, int? filterConditionId)
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);
            if (!userInfo.IsAuthenticated)
            {
                return new List<ACCompany>();
            }
            int?[]? documentStatusIds =await _commonsServices.getDocumentStatusByParam(operationType);
            if (documentStatusIds == null) return new List<ACCompany>();
            switch (filterConditionId)
            {
                case ((int?)FilterConditions.acCompany_ApplicationConfiguration_SolutionSetup):
                    return await _eRPOSContext.ACCompany.AsNoTracking()
                        .Where(x =>
                        x.Status == true
                        && documentStatusIds.Contains(x.DocumentStatus)).Select(x => new ACCompany
                        {
                            Id = x.Id,
                            GuID = x.GuID,
                            Description = x.Description
                        }).ToListAsync();
                default:
                    return new List<ACCompany>();
            }
        }
        public async Task<List<ACBranch>> populateBranchByParam(string? operationType, int? filterConditionId, int? companyId)
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);
            if (!userInfo.IsAuthenticated)
            {
                return new List<ACBranch>();
            }

            int?[]? documentStatusIds = await _commonsServices.getDocumentStatusByParam(operationType);
            if (documentStatusIds == null) return new List<ACBranch>();
            switch (filterConditionId)
            {
                case ((int?)FilterConditions.acBranch_ApplicationConfiguration_SolutionSetup):
                    return await _eRPOSContext.ACBranch.AsNoTracking()
                        .Where(x =>
                        x.CompanyId == companyId
                        && x.Status == true
                        && documentStatusIds.Contains(x.DocumentStatus)).Select(x => new ACBranch
                        {
                            Id = x.Id,
                            GuID = x.GuID,
                            Description = x.Description
                        }).ToListAsync();
                case ((int?)FilterConditions.acBranch_Operation_ByAllowedBranches):
                    var allowedBranchIds = userInfo.AllowedBranchIds?.Split(",").Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => Convert.ToInt32(x)).ToList();
                    return await _eRPOSContext.ACBranch.AsNoTracking()
                        .Where(x =>
                        x.CompanyId == userInfo.CompanyId
                        && x.Status == true
                        && allowedBranchIds.Contains(x.Id)
                        && documentStatusIds.Contains(x.DocumentStatus)).Select(x => new ACBranch
                        {
                            Id = x.Id,
                            GuID = x.GuID,
                            Description = x.Description
                        }).ToListAsync();
                default:
                    return new List<ACBranch>();
            }
        }
        public async Task<List<ACSaleUnit>> populateSaleUnitByParam(string? operationType, int? filterConditionId)
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);
            if (!userInfo.IsAuthenticated)
            {
                return new List<ACSaleUnit>();
            }

            int?[]? documentStatusIds = await _commonsServices.getDocumentStatusByParam(operationType);
            if (documentStatusIds == null) return new List<ACSaleUnit>();
            switch (filterConditionId)
            {
                case ((int?)FilterConditions.acSaleUnit_Operation_ByCompany):
                    return await _eRPOSContext.ACSaleUnit.AsNoTracking()
                        .Where(x =>
                        x.CompanyId == userInfo.CompanyId
                        && x.Status == true
                        && documentStatusIds.Contains(x.DocumentStatus)).Select(x => new ACSaleUnit
                        {
                            Id = x.Id,
                            GuID = x.GuID,
                            Description = x.Description
                        }).ToListAsync();
      
                default:
                    return new List<ACSaleUnit>();
            }
        }

      
    }

}
