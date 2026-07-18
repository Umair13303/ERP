using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganisationSetup.Areas.AccountNfinance.Services;
using OrganisationSetup.Areas.ApplicationConfiguration.Services;
using OrganisationSetup.Areas.Inventory.Services;
using OrganisationSetup.Areas.Procurement.Services;
using OrganisationSetup.Areas.SaleOperation.Services;
using OrganisationSetup.Services;
using SharedUI.Models.Configurations;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;

namespace OrganisationSetup.Areas.AccountNfinance.Controllers
{
    [Authorize]
    [Area(nameof(SetupRoute.Area.AccountNfinance))]
    public class AFInvoiceManagementController : Controller
    {
        private readonly ICommon _commonsServices;
        private readonly TempUser _currentUser;
        private readonly ISaleOperationRetriever _sorService;
        private readonly IApplicationConfigurationRetriever _acrService;
        private readonly IInventoryRetriever _irService;
        private readonly IAccountNfinanceUpsert _anfuService;
        private readonly IAccountNfinanceRetriever _anfrService;

        public AFInvoiceManagementController(ICommon commonsServices, TempUser currentUser, IApplicationConfigurationRetriever acrService, IInventoryRetriever irService, ISaleOperationRetriever sorService, IAccountNfinanceUpsert anfuService, IAccountNfinanceRetriever anfrService)
        {
            _commonsServices = commonsServices;
            _currentUser = currentUser;
            _acrService = acrService;
            _irService = irService;
            _sorService = sorService;
            _anfuService = anfuService;
            _anfrService = anfrService;
        }

        #region PORTION CONTAIN CODE TO: RENDER VIEW

        public IActionResult CreateUpdate_AFInvoice_DIR_UI(UISetting ui)
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
        public async Task<IActionResult> populateCustomerListByParam(string operationType)
        {
            var result = await _sorService.populateCustomerByParam(operationType, (int?)FilterConditions.SOCustomer_Operation_ByCompany);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populateProductListByParam(string operationType, string searchParam)
        {
            var result = await _irService.populateProductByParam(operationType, (int?)FilterConditions.IProduct_Operation_ALLActive_ByCompany, searchParam);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> populateCustomerSummListByParam(string operationType, int?[]? customerIds = null)
        {
            try
            {
                var result = await _sorService.populateCustomerSummByParam(operationType, customerIds);
                return Json(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { data = new List<object>(), message = ex.Message });
            }
        }
        #endregion

        #region PORTION FOR :: ADD/EDIT/DELETE DOCUMENT
        [HttpPost]
        public async Task<IActionResult> createUpdateInvoice([FromBody] PostedData postedData)
        {
            var result = await _anfuService.updateInsertDataInto_AFInvoice(postedData);
            return Json(new { result.IsSuccess, responseCode = result.StatusCode, message = result.Message });
        }
        #endregion
    }
}
