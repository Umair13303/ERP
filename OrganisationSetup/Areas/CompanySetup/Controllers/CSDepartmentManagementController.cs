using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganisationSetup.Areas.CompanySetup.Services;
using OrganisationSetup.Services;
using SharedUI.Models.Configurations;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;

namespace OrganisationSetup.Areas.CompanySetup.Controllers
{
    [Authorize]
    [Area(nameof(SetupRoute.Area.CompanySetup))]
    public class CSDepartmentManagementController : Controller
    {
        private readonly ICompanySetupUpsert _acuService;
        private readonly ICompanySetupRetriever _acrService;

        public CSDepartmentManagementController(ICompanySetupUpsert acCompanyService, ICompanySetupRetriever acrService)
        {
            _acuService = acCompanyService;
            _acrService = acrService;

        }

        public IActionResult CreateUpdate_CSDepartment_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            return View();
        }


        #region PORTION CONTAIN CODE TO: ADD/EDIT/DELETE DOCUMENT
        [HttpPost]
        public async Task<IActionResult> createUpdateDepartment([FromBody] PostedData postedData)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            if (!ModelState.IsValid) return View(postedData);

            var result = await _acuService.updateInsertDataInto_CSDepartment(postedData);
            return Json(new { result.IsSuccess, responseCode = result.StatusCode, message = result.Message });
        }
        #endregion
    }
}
