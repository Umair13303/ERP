using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganisationSetup.Areas.AccountNfinance.Services;
using OrganisationSetup.Areas.ApplicationConfiguration.Services;
using OrganisationSetup.Areas.CompanySetup.Services;
using OrganisationSetup.Areas.Inventory.Services;
using OrganisationSetup.Services;
using SharedUI.Models.Configurations;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;

namespace OrganisationSetup.Areas.Inventory.Controllers
{
    [Authorize]
    [Area(nameof(SetupRoute.Area.Inventory))]
    public class IProductManagementController : Controller
    {
        private readonly IInventoryRetriever _IrService;
        private readonly ICompanySetupRetriever _CSrService;
        private readonly IInventoryUpsert _IuService;
        private readonly IApplicationConfigurationRetriever _IacrService;
        private readonly IAccountNfinanceRetriever _IafService;
        private readonly ICommon _IcService;
        public IProductManagementController(IInventoryRetriever irService, ICompanySetupRetriever csrService, IInventoryUpsert iuService, IApplicationConfigurationRetriever IacrService, ICommon IcService, IAccountNfinanceRetriever IafService)
        {
            _IrService = irService;
            _IuService = iuService;
            _CSrService = csrService;
            _IcService = IcService;
            _IacrService = IacrService;
            _IafService = IafService;
        }

        #region PORTION CONTAIN CODE TO: RENDER VIEW
        public IActionResult CreateUpdate_IProduct_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            return View();
        }
        #endregion


        #region PORTION CONTAIN CODE TO: RETURN DEPENDING DDL
        [HttpGet]
        public async Task<IActionResult> populatevAttributeListByParam()
        {
            var result = await _IcService.populateAttributeByParam();
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populateBrandListByParam(string operationType)
        {
            var result = await _IrService.populateBrandByParam(operationType, (int?)FilterConditions.IBrand_Operation_ByCompany);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populateDepartmentListByParam(string operationType)
        {
            var result = await _CSrService.populateDepartmentByParam(operationType, (int?)FilterConditions.CSDepartment_Operation_ByCompany);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populateSectionListByParam(string operationType, int? departmentId)
        {
            var result = await _IrService.populateSectionByParam(operationType, (int?)FilterConditions.ISection_Operation_ByDepartment, departmentId);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populateCategoryListByParam(string operationType, int? sectionId)
        {
            var result = await _IrService.populateCategoryByParam(operationType, (int?)FilterConditions.ICategory_Operation_BySection, sectionId);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populateSubCategoryListByParam(string operationType, int? categoryId)
        {
            var result = await _IrService.populateSubCategoryByParam(operationType, (int?)FilterConditions.ISubCategory_Operation_ByCategory, categoryId);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populateSaleUnitListByParam(string operationType)
        {
            var result = await _IacrService.populateSaleUnitByParam(operationType, (int?)FilterConditions.acSaleUnit_Operation_ByCompany);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populateInventoryAccountListByParam(string operationType)
        {
            var result = await _IcService.populateOSvChartOfAccountByParam(operationType, (int?)FilterConditions.osvChartOfAccount_Operation_ByDefaultSetting, (int?)AccountCategory.INVENTORY);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populateSaleRevenueAccountListByParam(string operationType)
        {
            var result = await _IcService.populateOSvChartOfAccountByParam(operationType, (int?)FilterConditions.osvChartOfAccount_Operation_ByDefaultSetting, (int?)AccountCategory.SALES__REVENUES);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populateCostOfSaleAccountListByParam(string operationType)
        {
            var result = await _IcService.populateOSvChartOfAccountByParam(operationType,(int?)FilterConditions.osvChartOfAccount_Operation_ByDefaultSetting, (int?)AccountCategory.COST_OF_SALES);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populatevItemTypeListByParam()
        {
            var result = await _IcService.populateItemTypeByParam();
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populatevHSCodeListByParam()
        {
            var result = await _IcService.populateHSCodeByParam();
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populatevSaleTaxTypeListByParam()
        {
            var result = await _IcService.populateSaleTaxTypeByParam();
            return Json(result);
        }

        #endregion


        #region PORTION CONTAIN CODE TO: ADD/EDIT/DELETE DOCUMENT
        [HttpPost]
        public async Task<IActionResult> createUpdateProduct([FromBody] PostedData postedData)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            if (!ModelState.IsValid) return View(postedData);

            var result = await _IuService.updateInsertDataInto_IProduct(postedData);
            return Json(new { result.IsSuccess, responseCode = result.StatusCode, message = result.Message });
        }
        #endregion
    }
}
