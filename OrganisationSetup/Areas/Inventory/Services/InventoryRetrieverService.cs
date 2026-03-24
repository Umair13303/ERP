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

namespace OrganisationSetup.Areas.Inventory.Services
{
    public interface IInventoryRetriever
    {
        Task<List<ISection>> populateSectionByParam(string? operationType, int? filterConditionId, int? departmentId);
        Task<List<ICategory>> populateCategoryByParam(string? operationType, int? filterConditionId, int? sectionId);
        Task<List<ISubCategory>> populateSubCategoryByParam(string? operationType, int? filterConditionId, int? categoryId);
        Task<List<IBrand>> populateBrandByParam(string? operationType, int? filterConditionId);
    }
    public class InventoryRetrieverService : IInventoryRetriever
    {
        private readonly ERPOrganisationSetupContext _eRPOSContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICommon _commonsServices;


        public InventoryRetrieverService(ERPOrganisationSetupContext eRPOSC, IHttpContextAccessor httpContextAccessor, ICommon commonsServices)
        {
            _eRPOSContext = eRPOSC;
            _httpContextAccessor = httpContextAccessor;
            _commonsServices = commonsServices;

        }
        public async Task<List<ISection>> populateSectionByParam(string? operationType, int? filterConditionId, int? departmentId)
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);
            if (!userInfo.IsAuthenticated)
            {
                return new List<ISection>();
            }

            int?[]? documentStatusIds = await _commonsServices.getDocumentStatusByParam(operationType);
            if (documentStatusIds == null) return new List<ISection>();
            switch (filterConditionId)
            {
                case ((int?)FilterConditions.ISection_Operation_ByDepartment):
                    return await _eRPOSContext.ISection.AsNoTracking()
                        .Where(x =>
                        x.CompanyId == userInfo.CompanyId
                        && x.DepartmentId == departmentId
                        && x.Status == true
                        && documentStatusIds.Contains(x.DocumentStatus)).Select(x => new ISection
                        {
                            Id = x.Id,
                            GuID = x.GuID,
                            Description = x.Description
                        }).ToListAsync();
                default:
                    return new List<ISection>();
            }
        }
        public async Task<List<ICategory>> populateCategoryByParam(string? operationType, int? filterConditionId, int? sectionId)
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);
            if (!userInfo.IsAuthenticated)
            {
                return new List<ICategory>();
            }

            int?[]? documentStatusIds = await _commonsServices.getDocumentStatusByParam(operationType);
            if (documentStatusIds == null) return new List<ICategory>();
            switch (filterConditionId)
            {
                case ((int?)FilterConditions.ICategory_Operation_BySection):
                    return await _eRPOSContext.ICategory.AsNoTracking()
                        .Where(x =>
                        x.CompanyId == userInfo.CompanyId
                        && x.SectionId == sectionId
                        && x.Status == true
                        && documentStatusIds.Contains(x.DocumentStatus)).Select(x => new ICategory
                        {
                            Id = x.Id,
                            GuID = x.GuID,
                            Description = x.Description
                        }).ToListAsync();
                default:
                    return new List<ICategory>();
            }
        }
        public async Task<List<ISubCategory>> populateSubCategoryByParam(string? operationType, int? filterConditionId, int? categoryId)
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);
            if (!userInfo.IsAuthenticated)
            {
                return new List<ISubCategory>();
            }

            int?[]? documentStatusIds = await _commonsServices.getDocumentStatusByParam(operationType);
            if (documentStatusIds == null) return new List<ISubCategory>();
            switch (filterConditionId)
            {
                case ((int?)FilterConditions.ISubCategory_Operation_ByCategory):
                    return await _eRPOSContext.ISubCategory.AsNoTracking()
                        .Where(x =>
                        x.CompanyId == userInfo.CompanyId
                        && x.CategoryId == categoryId
                        && x.Status == true
                        && documentStatusIds.Contains(x.DocumentStatus)).Select(x => new ISubCategory
                        {
                            Id = x.Id,
                            GuID = x.GuID,
                            Description = x.Description
                        }).ToListAsync();
                default:
                    return new List<ISubCategory>();
            }
        }
        public async Task<List<IBrand>> populateBrandByParam(string? operationType, int? filterConditionId)
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);
            if (!userInfo.IsAuthenticated)
            {
                return new List<IBrand>();
            }

            int?[]? documentStatusIds = await _commonsServices.getDocumentStatusByParam(operationType);
            if (documentStatusIds == null) return new List<IBrand>();
            switch (filterConditionId)
            {
                case ((int?)FilterConditions.IBrand_Operation_ByCompany):
                    return await _eRPOSContext.IBrand.AsNoTracking()
                        .Where(x =>
                        x.CompanyId == userInfo.CompanyId
                        && x.Status == true
                        && documentStatusIds.Contains(x.DocumentStatus)).Select(x => new IBrand
                        {
                            Id = x.Id,
                            GuID = x.GuID,
                            Description = x.Description
                        }).ToListAsync();
                default:
                    return new List<IBrand>();
            }
        }
        public async Task<List<DTObject.SubCategory_List>> populateSubCategoryByParam()
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);

            if (!userInfo.IsAuthenticated)
            {
                return new List<SubCategory_List>();
            }
            var result = await (
                from sc in _eRPOSContext.ISubCategory
                join c in _eRPOSContext.ICategory on sc.CategoryId equals c.Id
                where (sc.Status == true && c.Status == true )
                select new DTObject.SubCategory_List
                {
                    Category = c.Description,
                    SubCategoryId = sc.Id,
                    SubCategory = sc.Description
                }
            ).ToListAsync();

            return result;
        }
    }

}
