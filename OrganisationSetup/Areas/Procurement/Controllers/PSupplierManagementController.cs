using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganisationSetup.Areas.Inventory.Services;
using OrganisationSetup.Areas.Procurement.Services;
using OrganisationSetup.Services;
using SharedUI.Models.Configurations;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;

namespace OrganisationSetup.Areas.Procurement.Controllers
{

    [Authorize]
    [Area(nameof(SetupRoute.Area.Procurement))]
    public class PSupplierManagementController : Controller
    {

        private readonly IProcurementUpsert _IuService;
        private readonly IProcurementRetriever _IprService;
        public PSupplierManagementController(IProcurementUpsert IuService, IProcurementRetriever IprService, ICommon commonsServices)
        {
            _IuService = IuService;
            _IprService = IprService;
        }
        #region PORTION CONTAIN CODE TO: RENDER VIEW
        public IActionResult CreateUpdate_PSupplier_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            return View();
        }
        #endregion

        #region PORTION CONTAIN CODE TO: RETURN RECORD LIST
        [HttpGet]
        public async Task<IActionResult> populateSupplierSummListByParam(string operationType, int?[]? supplierIds = null)
        {
            try
            {
                var result = await _IprService.populateSupplierSummByParam(operationType, supplierIds);
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
        public async Task<IActionResult> createUpdateSupplier([FromBody] PostedData postedData)
        {
            var result = await _IuService.updateInsertDataInto_PSupplier(postedData);
            return Json(new { result.IsSuccess, responseCode = result.StatusCode, message = result.Message });
        }
        #endregion
    }
}
