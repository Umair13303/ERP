using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganisationSetup.Areas.AccountNfinance.Services;
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
    public class ICategoryManagementController : Controller
    {
        private readonly IInventoryRetriever _IrService;
        private readonly ICompanySetupRetriever _CSrService;
        private readonly IInventoryUpsert _IuService;

        public ICategoryManagementController(IInventoryRetriever irService, ICompanySetupRetriever csrService, IInventoryUpsert iuService)
        {
            _IrService = irService;
            _IuService = iuService;
            _CSrService = csrService;
        }

        #region PORTION CONTAIN CODE TO: RENDER VIEW
        public IActionResult CreateUpdate_ICategory_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            return View();
        }
        public IActionResult CreateUpdate_ISubCategory_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            return View();
        }
        #endregion
        #region PORTION CONTAIN CODE TO: RETURN DEPENDING DDL
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
        #endregion
        #region PORTION CONTAIN CODE TO: ADD/EDIT/DELETE DOCUMENT
        [HttpPost]
        public async Task<IActionResult> createUpdateCategory([FromBody] PostedData postedData)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            if (!ModelState.IsValid) return View(postedData);

            var result = await _IuService.updateInsertDataInto_ICategory(postedData);
            return Json(new { result.IsSuccess, responseCode = result.StatusCode, message = result.Message });
        }
        [HttpPost]
        public async Task<IActionResult> createUpdateSubCategory([FromBody] PostedData postedData)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            if (!ModelState.IsValid) return View(postedData);

            var result = await _IuService.updateInsertDataInto_ISubCategory(postedData);
            return Json(new { result.IsSuccess, responseCode = result.StatusCode, message = result.Message });
        }
        #endregion
    }
}
