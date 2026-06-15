using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Areas.AccountNfinance.Services;
using OrganisationSetup.Areas.ApplicationConfiguration.Services;
using OrganisationSetup.Areas.CompanySetup.Services;
using OrganisationSetup.Areas.Procurement.Services;
using OrganisationSetup.Areas.SaleOperation.Services;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Services;
using SharedUI.Models.Configurations;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;

namespace OrganisationSetup.Areas.AccountNfinance.Controllers
{

    [Authorize]
    [Area(nameof(SetupRoute.Area.AccountNfinance))]
    public class AFProductPriceLogManagementController : Controller
    {

        private readonly ICommon _commonsServices;
        private readonly IProcurementRetriever _prService;
        private readonly IApplicationConfigurationRetriever _acrService;
        private readonly IAccountNfinanceRetriever _anfrService;
        private readonly TempUser _currentUser;
        private readonly IAccountNfinanceUpsert _anfuService;


        public AFProductPriceLogManagementController(ICommon commonsServices, IProcurementRetriever prService, IApplicationConfigurationRetriever acrService, IAccountNfinanceRetriever anfrService, TempUser currentUser, IAccountNfinanceUpsert anfuService)
        {
            _commonsServices = commonsServices;
            _prService = prService;
            _acrService = acrService;
            _anfrService = anfrService;
            _currentUser = currentUser;
            _anfuService = anfuService;
        }
        //PRODWGEN :: PRODUCT WISE GENERATOR
        //BULKWGEN :: ALL PRODUCT WISE GENERATOR
        #region PORTION CONTAIN CODE TO: RENDER VIEW
        public IActionResult CreateUpdate_AFProductPriceLog_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            ViewBag.LocationId = _currentUser.BranchId; 
            return View();
        }
        #endregion
    }
}
