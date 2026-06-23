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
        private readonly ICommon _IcService;


        public SOCustomerManagementController(ISaleOperationUpsert souService, ISaleOperationRetriever sorService, ICommon icService    )
        {
            _souService = souService;
            _sorService = sorService;
            _IcService = icService;
        }
        #region PORTION CONTAIN CODE TO: RENDER VIEW
        public IActionResult CreateUpdate_SOCustomer_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            return View();
        }
        #endregion

        #region PORTION CONTAIN CODE TO: RETURN DEPENDING DDL
        [HttpGet]
        public async Task<IActionResult> populatevTierTypeListByParam()
        {
            var result = await _IcService.populateTierTypeByParam();
            return Json(result);
        }
        #endregion

        #region PORTION CONTAIN CODE TO: RETURN RECORD LIST
        [HttpGet]
        public async Task<IActionResult> populateCustomerSummListByParam(string operationType, int?[]? customerIds=null)
        {
            try
            {
                var result = await _sorService.populateCustomerSummByParam(operationType, customerIds);
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
            var result = await _souService.updateInsertDataInto_SOCustomer(postedData);
            return Json(new { result.IsSuccess, responseCode = result.StatusCode, message = result.Message });
        }
        #endregion
    }
}
