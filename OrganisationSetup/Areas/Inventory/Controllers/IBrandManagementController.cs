using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganisationSetup.Areas.Inventory.Services;
using OrganisationSetup.Services;
using SharedUI.Models.Configurations;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;

namespace OrganisationSetup.Areas.Inventory.Controllers
{
    [Authorize]
    [Area(nameof(SetupRoute.Area.Inventory))]
    public class IBrandManagementController : Controller
    {
        private readonly IInventoryUpsert _acuService;
        private readonly ICommon _commonsServices;
        public IBrandManagementController(IInventoryUpsert acCompanyService, ICommon commonsServices)
        {
            _commonsServices = commonsServices;
            _acuService = acCompanyService;
        }
        #region PORTION CONTAIN CODE TO: RENDER VIEW
        public IActionResult CreateUpdate_IBrand_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            return View();
        }
        #endregion


        #region PORTION CONTAIN CODE TO: ADD/EDIT/DELETE DOCUMENT
        [HttpPost]
        public async Task<IActionResult> createUpdateBrand([FromBody] PostedData postedData)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            if (!ModelState.IsValid) return View(postedData);

            var result = await _acuService.updateInsertDataInto_IBrand(postedData);
            return Json(new { result.IsSuccess, responseCode = result.StatusCode, message = result.Message });
        }
        #endregion
    }
}
