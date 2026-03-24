using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganisationSetup.Areas.CompanySetup.Services;
using OrganisationSetup.Areas.Inventory.Services;
using OrganisationSetup.Services;
using SharedUI.Models.Configurations;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;
using System.ComponentModel.Design;

namespace OrganisationSetup.Areas.Inventory.Controllers
{
    [Authorize]
    [Area(nameof(SetupRoute.Area.Inventory))]
    public class ISectionManagementController : Controller
    {
        private readonly IInventoryUpsert _IuService;
        private readonly IInventoryRetriever _IrService;
        private readonly ICompanySetupRetriever _CSrService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ISectionManagementController(IInventoryUpsert acuService, IInventoryRetriever acrService, ICompanySetupRetriever csrService, ICommon commonsServices, IHttpContextAccessor httpContextAccessor)
        {
            _IuService = acuService;
            _IrService = acrService;
            _CSrService = csrService;
            _httpContextAccessor = httpContextAccessor;
        }

        #region PORTION CONTAIN CODE TO: RENDER VIEW
        public IActionResult CreateUpdate_ISection_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            return View();
        }
        #endregion

        #region PORTION CONTAIN CODE TO: RETURN DEPENDING DDL
        [HttpGet]
        public async Task<IActionResult> populateDepartmentListByParam(string operationType)
        {
            var result = await _CSrService.populateDepartmentByParam(operationType, (int?)FilterConditions.CSDepartment_Operation_ByCompany);
            return Json(result);
        }
        #endregion


        #region PORTION CONTAIN CODE TO: ADD/EDIT/DELETE DOCUMENT
        [HttpPost]
        public async Task<IActionResult> createUpdateSection([FromBody] PostedData postedData)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            if (!ModelState.IsValid) return View(postedData);

            var result = await _IuService.updateInsertDataInto_ISection(postedData);
            return Json(new { result.IsSuccess, responseCode = result.StatusCode, message = result.Message });
        }
        #endregion
    }
}
