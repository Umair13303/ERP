using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganisationSetup.Areas.ApplicationConfiguration.Services;
using OrganisationSetup.Areas.Inventory.Services;
using OrganisationSetup.Services;
using SharedUI.Models.Configurations;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;

namespace OrganisationSetup.Areas.Inventory.Controllers
{

    [Authorize]
    [Area(nameof(SetupRoute.Area.Inventory))]
    public class ITransferManagementController : Controller
    {
        private readonly IInventoryUpsert _acuService;
        private readonly ICommon _commonsServices;
        private readonly IInventoryRetriever _IrService;
        private readonly IApplicationConfigurationRetriever _acrService;


        public ITransferManagementController(IInventoryUpsert acCompanyService, ICommon commonsServices, IInventoryRetriever irService, IApplicationConfigurationRetriever acrService)
        {
            _commonsServices = commonsServices;
            _acuService = acCompanyService;
            _IrService = irService;
            _acrService = acrService;
        }
        #region PORTION CONTAIN CODE TO: RENDER VIEW
        public IActionResult CreateUpdate_IBranchTransfer_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            return View();
        }
        #endregion

        #region DROPDOWN ENDPOINTS
        [HttpGet]
        public async Task<IActionResult> populateSourceBranchListByParam(string operationType)
        {
            var result = await _acrService.populateBranchByParam(operationType, (int?)FilterConditions.acBranch_Operation_ByAllowedBranches, null);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populateDestinationBranchListByParam(string operationType)
        {
            var result = await _acrService.populateBranchByParam(operationType, (int?)FilterConditions.acBranch_ApplicationConfiguration_SolutionSetup, null);
            return Json(result);
        }
        #endregion
    }
}
