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
        public PSupplierManagementController(IProcurementUpsert IuService, ICommon commonsServices)
        {
            _IuService = IuService;
        }
        #region PORTION CONTAIN CODE TO: RENDER VIEW
        public IActionResult CreateUpdate_PSupplier_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            return View();
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
