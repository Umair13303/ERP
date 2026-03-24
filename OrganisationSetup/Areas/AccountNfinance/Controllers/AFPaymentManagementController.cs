using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganisationSetup.Areas.CompanySetup.Services;
using OrganisationSetup.Areas.SaleOperation.Services;
using OrganisationSetup.Services;
using SharedUI.Models.Configurations;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;

namespace OrganisationSetup.Areas.AccountNfinance.Controllers
{

    [Authorize]
    [Area(nameof(SetupRoute.Area.AccountNfinance))]
    public class AFPaymentManagementController : Controller
    {

        private readonly ICommon _commonsServices;
        private readonly ISaleOperationUpsert _souService;

        public AFPaymentManagementController(ICommon commonsServices, ISaleOperationUpsert souService)
        {
            _commonsServices = commonsServices;
            _souService = souService;
        }
        #region PORTION CONTAIN CODE TO: RENDER VIEW
        public IActionResult CreateUpdate_AFPaymentReceipt_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            return View();
        }
        #endregion
    }
}
