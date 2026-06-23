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
        Task<List<Product_List>> populateProductByParam(string? operationType, int? filterConditionId, string searchParam);
        Task<List<Product_Master_List>> populateProductMasterBySearch(int brandId, int sectionId, int categoryId, int subCategoryId, int productTypeId, bool? status = true);
        Task<List<Category_Master_List>> populateCategoryMasterBySearch(int? departmentId, int? sectionId, bool? status = true);
        Task<List<SubCategory_Master_List>> populateSubCategoryMasterBySearch(int? departmentId, int? sectionId,int? categoryId, bool? status = true);
    }
    public class InventoryRetrieverService : IInventoryRetriever
    {
        private readonly TempUser _currentUser;
        private readonly ERPOrganisationSetupContext _eRPOSContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICommon _commonsServices;


        public InventoryRetrieverService(TempUser currentUser, ERPOrganisationSetupContext eRPOSC, IHttpContextAccessor httpContextAccessor, ICommon commonsServices)
        {
            _currentUser = currentUser;
            _eRPOSContext = eRPOSC;
            _httpContextAccessor = httpContextAccessor;
            _commonsServices = commonsServices;

        }
        public async Task<List<ISection>> populateSectionByParam(string? operationType, int? filterConditionId, int? departmentId)
        {
            var userInfo = _currentUser;
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
                            DepartmentId = x.DepartmentId,
                            Description = x.Description
                        }).ToListAsync();
                default:
                    return new List<ISection>();
            }
        }
        public async Task<List<ICategory>> populateCategoryByParam(string? operationType, int? filterConditionId, int? sectionId)
        {
            var userInfo = _currentUser;
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
            var userInfo = _currentUser;
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
            var userInfo = _currentUser;
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
            var userInfo = _currentUser;

            if (!userInfo.IsAuthenticated)
            {
                return new List<SubCategory_List>();
            }
            var result = await (
                from sc in _eRPOSContext.ISubCategory
                join c in _eRPOSContext.ICategory on sc.CategoryId equals c.Id
                where (sc.Status == true && c.Status == true)
                select new DTObject.SubCategory_List
                {
                    Category = c.Description,
                    SubCategoryId = sc.Id,
                    SubCategory = sc.Description
                }
            ).ToListAsync();

            return result;
        }
        public async Task<List<Product_List>> populateProductByParam(string? operationType, int? filterConditionId, string searchParam)
        {
            var userInfo = _currentUser;
            if (!userInfo.IsAuthenticated)
            {
                return new List<Product_List>();
            }

            int?[]? documentStatusIds = await _commonsServices.getDocumentStatusByParam(operationType);
            if (documentStatusIds == null) return new List<Product_List>();
            switch (filterConditionId)
            {
                case ((int?)FilterConditions.IProduct_Operation_ALLActive_ByCompany):
                    return await (from x in _eRPOSContext.IProduct.AsNoTracking()
                                  where x.CompanyId == userInfo.CompanyId
                                     && x.Status == true
                                     && documentStatusIds.Contains(x.DocumentStatus)
                                  let price = _eRPOSContext.AFProductPriceLog.AsNoTracking()
                                      .Where(p => p.ProductId == x.Id && p.Status == true && p.CompanyId == userInfo.CompanyId)
                                      .OrderByDescending(p => p.CreatedOn)
                                      .Select(p => (decimal?)p.DefaultSalePrice)
                                      .FirstOrDefault()
                                  select new Product_List
                                  {
                                      Id = x.Id,
                                      Text = x.Description,
                                      AttIds = x.AttributeIds,
                                      IsExpiryApplied = x.IsExpiryApplicable,
                                      UnitSalePrice = price ?? 0m
                                  }).ToListAsync();
                default:
                    return new List<Product_List>();
            }
        }
        public async Task<List<Category_Master_List>> populateCategoryMasterBySearch(int? departmentId, int? sectionId, bool? status = true)
        {
            var userInfo = _currentUser;
            if (!userInfo.IsAuthenticated)
            {
                return new List<Category_Master_List>();
            }
            return await (from c in _eRPOSContext.ICategory.AsNoTracking().Where(c => c.CompanyId == userInfo.CompanyId && (c.Status == status || status== null) )
                          join s in _eRPOSContext.ISection.AsNoTracking().Where(s => s.Id == sectionId || sectionId == -1 && (s.Status == status || status == null))
                            on c.SectionId equals s.Id

                          select new Category_Master_List
                          {
                              GuID = c.GuID,
                              Code= c.Code,
                              Description = c.Description,
                              Section = s.Description,
                              DocumentStatus = c.DocumentStatus
                          }).ToListAsync();
        }
        public async Task<List<SubCategory_Master_List>> populateSubCategoryMasterBySearch(int? departmentId, int? sectionId,int? categoryId, bool? status = true)
        {
            var userInfo = _currentUser;
            if (!userInfo.IsAuthenticated)
            {
                return new List<SubCategory_Master_List>();
            }
            return await (from sc in _eRPOSContext.ISubCategory.AsNoTracking().Where(c => c.CompanyId == userInfo.CompanyId && (c.Status == status || status == null))
                          join c in _eRPOSContext.ICategory.AsNoTracking().Where(c => c.CompanyId == userInfo.CompanyId && (c.Status == status || status == null))
                            on sc.CategoryId equals c.Id
                          join s in _eRPOSContext.ISection.AsNoTracking().Where(s => s.Id == sectionId || sectionId == -1 && (s.Status == status || status == null))
                            on c.SectionId equals s.Id

                          select new SubCategory_Master_List
                          {
                              GuID = c.GuID,
                              Code = c.Code,
                              Description = sc.Description,
                              Category = c.Description,
                              Section = s.Description,
                              DocumentStatus = c.DocumentStatus
                          }).ToListAsync();
        }

        public async Task<List<Product_Master_List>> populateProductMasterBySearch(int brandId, int sectionId, int categoryId, int subCategoryId, int productTypeId, bool? status = true)
        {
            var userInfo = _currentUser;
            if (!userInfo.IsAuthenticated)
            {
                return new List<Product_Master_List>();
            }
            return await (from p in _eRPOSContext.IProduct.AsNoTracking().Where(p => p.CompanyId == userInfo.CompanyId && (p.Status == status || status == null))
                          join s in _eRPOSContext.ISection.AsNoTracking().Where(s => s.Id == sectionId || sectionId == -1 && (s.Status == status || status == null))
                            on p.SectionId equals s.Id
                          join c in _eRPOSContext.ICategory.AsNoTracking().Where(c => c.Id == categoryId || categoryId == -1 && (c.Status == status || status == null))
                            on p.CategoryId equals c.Id
                          join sc in _eRPOSContext.ISubCategory.AsNoTracking().Where(sc => sc.Id == subCategoryId || subCategoryId == -1 && (sc.Status == status || status == null))
                            on p.SubCategoryId equals sc.Id
                          join b in _eRPOSContext.IBrand.AsNoTracking().Where(b => b.Id == brandId || brandId == -1 && (b.Status == status || status == null))
                            on p.BrandId equals b.Id
                          join vPT in _eRPOSContext.vProductType.AsNoTracking().Where(vPT => vPT.Id == productTypeId || productTypeId == -1)
                            on p.ProductTypeId equals vPT.Id

                          select new Product_Master_List
                          {
                              GuID = p.GuID,
                              Code = p.Code,
                              Description = p.Description,
                              Category = c.Description,    
                              SubCategory = sc.Description,
                              Brand = b.Description,
                              ProductType = vPT.Description,
                              DocumentStatus = p.DocumentStatus
                          }).ToListAsync();
        }

    }

}