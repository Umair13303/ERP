using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganisationSetup.Areas.AccountNfinance.Services;
using OrganisationSetup.Areas.ApplicationConfiguration.Services;
using OrganisationSetup.Services;
using SharedUI.Models.Configurations;
using SharedUI.Models.Enums;

namespace OrganisationSetup.Areas.AccountNfinance.Controllers
{

    [Authorize]
    [Area(nameof(SetupRoute.Area.AccountNfinance))]
    public class AFChartOfAccountManagementController : Controller
    {
        private readonly IAccountNfinanceUpsert _anfuService;
        private readonly ICommon _commonsServices;
        public AFChartOfAccountManagementController(IAccountNfinanceUpsert anfuService, ICommon commonsServices)
        {
            _commonsServices = commonsServices;
            _anfuService = anfuService;
        }
        public IActionResult CreatedUpdate_AFChartOfAccount_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            return View();
        }

        #region PORTION CONTAIN CODE TO: RETURN DEPENDING DDL
        [HttpGet]
        public async Task<IActionResult> populatevAccountTypeListByParam()
        {
            var result = await _commonsServices.populateAccountTypeByParam();
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> populatevAccountCatagoryListByParam(int? accountTypeId)
        {
            var result = await _commonsServices.populateAccountCatagoryByParam(accountTypeId);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populatevFinacialStatementListByParam()
        {
            var result = await _commonsServices.populateFinancialStatementByParam();
            return Json(result);
        }
        #endregion
    }
}
