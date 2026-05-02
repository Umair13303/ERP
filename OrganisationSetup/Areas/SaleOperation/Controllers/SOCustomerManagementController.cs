using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganisationSetup.Areas.CompanySetup.Services;
using OrganisationSetup.Areas.SaleOperation.Services;
using OrganisationSetup.Services;
using SharedUI.Models.Configurations;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;

namespace OrganisationSetup.Areas.SaleOperation.Controllers
{

    [Authorize]
    [Area(nameof(SetupRoute.Area.SaleOperation))]
    public class SOCustomerManagementController : Controller
    {
        private readonly ISaleOperationUpsert _souService;
        private readonly ISaleOperationRetriever _sorService;

        public SOCustomerManagementController(ISaleOperationUpsert souService, ISaleOperationRetriever sorService)
        {
            _souService = souService;
            _sorService = sorService;
        }
        #region PORTION CONTAIN CODE TO: RENDER VIEW
        public IActionResult CreateUpdate_SOCustomer_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            return View();
        }
        #endregion

        #region PORTION CONTAIN CODE TO: RETURN RECORD LIST
        [HttpGet]
        public async Task<IActionResult> populateCustomerSummListByParam(string operationType)
        {
            try
            {
                var result = await _sorService.populateCustomerSummByParam(operationType);
                return Json(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { data = new List<object>(), message = ex.Message });
            }
        }
        #endregion

        #region PORTION CONTAIN CODE TO: ADD/EDIT/DELETE DOCUMENT
        [HttpPost]
        public async Task<IActionResult> createUpdateCustomer([FromBody] PostedData postedData)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            if (!ModelState.IsValid) return View(postedData);

            var result = await _souService.updateInsertDataInto_SOCustomer(postedData);
            return Json(new { result.IsSuccess, responseCode = result.StatusCode, message = result.Message });
        }
        #endregion
    }
}
