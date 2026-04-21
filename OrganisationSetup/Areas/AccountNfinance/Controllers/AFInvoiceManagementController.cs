using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Areas.ApplicationConfiguration.Services;
using OrganisationSetup.Areas.CompanySetup.Services;
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
    public class AFInvoiceManagementController : Controller
    {

        private readonly ICommon _commonsServices;
        private readonly ISaleOperationRetriever _sorService;
        private readonly IApplicationConfigurationRetriever _acrService;
        private readonly TempUser _currentUser;


        public AFInvoiceManagementController(ICommon commonsServices,ISaleOperationRetriever sorService, IApplicationConfigurationRetriever acrService, TempUser currentUser)
        {
            _commonsServices = commonsServices;
            _sorService = sorService;
            _acrService = acrService;
            _currentUser = currentUser;
        }
        #region PORTION CONTAIN CODE TO: RENDER VIEW
        public IActionResult CreateUpdate_AFOBInvoice_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            ViewBag.LocationId = _currentUser.BranchId; 
            return View();
        }
        #endregion

        #region PORTION CONTAIN CODE TO: RETURN DEPENDING DDL
        [HttpGet]
        public async Task<IActionResult> populateBranchListByParam(string operationType)
        {
            var result = await _acrService.populateBranchByParam(operationType, (int?)FilterConditions.acBranch_Operation_ByAllowedBranches, null);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populateCustomerListByParam(string operationType)
        {
            var result = await _sorService.populateCustomerByParam(operationType, (int?)FilterConditions.SOCustomer_Operation_ByCompany);
            return Json(result);
        }
        #endregion

        #region PORTION CONTAIN CODE TO: RETURN RECORD LIST
        [HttpGet]
        public async Task<IActionResult> populateInvoiceListByParam(int? customerId,int?[] invoiceStatusIds)
        {
            return Json(200);
        }    
        #endregion
    }
}
