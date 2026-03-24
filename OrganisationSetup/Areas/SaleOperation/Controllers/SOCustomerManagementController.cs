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
        private readonly ICommon _commonsServices;
        private readonly ISaleOperationUpsert _souService;

        public SOCustomerManagementController( ICommon commonsServices, ISaleOperationUpsert souService)
        {
            _commonsServices = commonsServices;
            _souService = souService;
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
        public async Task<IActionResult> populatevCountryListByParam()
        {
            var result = await _commonsServices.populateCountryByParam();
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populatevCityListByParam(int? countryId)
        {
            var result = await _commonsServices.populateCityByParam(countryId);
            return Json(result);
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
