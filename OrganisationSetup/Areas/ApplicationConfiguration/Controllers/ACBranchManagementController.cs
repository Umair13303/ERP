using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganisationSetup.Areas.ApplicationConfiguration.Services;
using OrganisationSetup.Services;
using SharedUI.Models.Configurations;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;

namespace OrganisationSetup.Areas.ApplicationConfiguration.Controllers
{
    [Authorize]
    [Area(nameof(SetupRoute.Area.ApplicationConfiguration))]
    public class ACBranchManagementController : Controller
    {
        private readonly IApplicationConfigurationUpsert _acuService;
        private readonly ICommon _commonsServices;
        public ACBranchManagementController(IApplicationConfigurationUpsert acCompanyService, ICommon commonsServices)
        {
            _commonsServices = commonsServices;
            _acuService = acCompanyService;
        }
        #region PORTION CONTAIN CODE TO: RENDER VIEW
        public IActionResult CreateUpdate_ACBranch_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            return View();
        }
        #endregion

        #region PORTION CONTAIN CODE TO: RETURN DEPENDING DDL
        [HttpGet]
        public async Task<IActionResult> populatevOrganisationTypeListByParam()
        {
            var result = await _commonsServices.populateOrganisationTypeByParam();
            return Json(result);
        }
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
        public async Task<IActionResult> createUpdateBranch([FromBody] PostedData postedData)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            if (!ModelState.IsValid) return View(postedData);

            var result = await _acuService.updateInsertDataInto_ACBranch(postedData);
            return Json(new { IsSuccess = result.IsSuccess, responseCode = result.StatusCode, message = result.Message });
        }
        #endregion
    }
}
