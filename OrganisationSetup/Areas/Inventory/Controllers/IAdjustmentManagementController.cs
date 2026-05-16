using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganisationSetup.Areas.ApplicationConfiguration.Services;
using OrganisationSetup.Areas.Inventory.Services;
using OrganisationSetup.Services;
using SharedUI.Models.Configurations;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;

namespace OrganisationSetup.Areas.Inventory.Controllers
{

    [Authorize]
    [Area(nameof(SetupRoute.Area.Inventory))]
    public class IAdjustmentManagementController : Controller
    {
        private readonly ICommon _commonsServices;
        private readonly TempUser _currentUser;
        private readonly IApplicationConfigurationRetriever _acrService;
        private readonly IInventoryRetriever _irService;

        public IAdjustmentManagementController(ICommon commonsServices, TempUser currentUser, IApplicationConfigurationRetriever acrService, IInventoryRetriever irService)
        {
            _commonsServices = commonsServices;
            _currentUser = currentUser;
            _acrService = acrService;
            _irService = irService;
        }

        #region PORTION CONTAIN CODE TO: RENDER VIEW
        public IActionResult CreateUpdate_IAdjustment_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            ViewBag.LocationId = _currentUser.BranchId;
            return View();
        }
        #endregion

        #region PORTION CONTAIN CODE TO: RETURN DEPENDING DDL
        [HttpGet]
        public async Task<IActionResult> populateBranchListByParam(string operationType)
        {
            var result = await _acrService.populateBranchByParam(operationType, (int?)FilterConditions.acBranch_Operation_ByAllowedBranches, null);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populatevStockAdjustmentTypeListByParam()
        {
            var result = await _commonsServices.populateStockAdjustmentTypeByParam();
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populatevAttributeListByParam()
        {
            var result = await _commonsServices.populateAttributeByParam();
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populateProductListByParam(string operationType, string searchParam)
        {
            var result = await _irService.populateProductByParam(operationType, (int?)FilterConditions.IProduct_Operation_ALLActive_ByCompany, searchParam);
            return Json(result);
        }
        #endregion
    }
}
