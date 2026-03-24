using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganisationSetup.Areas.ApplicationConfiguration.Services;
using OrganisationSetup.Services;
using SharedUI.Models.Configurations;
using SharedUI.Models.Enums;
using SharedUI.Models.Responses;
using SharedUI.Models.SQLParameters;

namespace OrganisationSetup.Areas.ApplicationConfiguration.Controllers
{
    [Authorize]
    [Area(nameof(SetupRoute.Area.ApplicationConfiguration))]
    public class ACUserManagementController : Controller
    {
        private readonly IApplicationConfigurationUpsert _acuService;
        private readonly IApplicationConfigurationRetriever _acrService;
        private readonly ICommon _commonsServices;
        public ACUserManagementController(IApplicationConfigurationUpsert acuService, IApplicationConfigurationRetriever acrService, ICommon commonsServices)
        {
            _commonsServices = commonsServices;
            _acuService = acuService;
            _acrService = acrService;
        }
        #region PORTION CONTAIN CODE TO: RENDER VIEW
        public IActionResult CreateUpdate_ACUser_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            return View();
        }
        #endregion

        #region PORTION CONTAIN CODE TO: RETURN DEPENDING DDL
        [HttpGet]
        public async Task<IActionResult> populatevRoleListByParam(string operationType)
        {
            var result = await _commonsServices.populateRoleByParam();
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populateEmployeeListByParam(string operationType,int? companyId)
        {
            var result = Message.ddlEmployee;
            return await Task.FromResult(Json(result));
        }
        [HttpGet]
        public async Task<IActionResult> populateCompanyListByParam(string operationType)
        {
            var result = await _acrService.populateCompanyByParam(operationType,(int?)FilterConditions.acCompany_ApplicationConfiguration_SolutionSetup);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populateBranchListByParam(string operationType, int? companyId)
        {
            var result = await _acrService.populateBranchByParam(operationType, (int?)FilterConditions.acBranch_ApplicationConfiguration_SolutionSetup, companyId);
            return Json(result);
        }
        #endregion

        #region PORTION CONTAIN CODE TO: ADD/EDIT/DELETE DOCUMENT
        [HttpPost]
        public async Task<IActionResult> createUpdateUser([FromBody] PostedData postedData)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            if (!ModelState.IsValid) return View(postedData);

            var result = await _acuService.updateInsertDataInto_ACUser(postedData);
            return Json(new { IsSuccess = result.IsSuccess, responseCode = result.StatusCode, message = result.Message });
        }
        #endregion
    }
}
