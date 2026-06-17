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
    public class AFBillReceiptManagementController : Controller
    {

        private readonly ICommon _commonsServices;
        private readonly IProcurementRetriever _prService;
        private readonly IApplicationConfigurationRetriever _acrService;
        private readonly IAccountNfinanceRetriever _anfrService;
        private readonly TempUser _currentUser;
        private readonly IAccountNfinanceUpsert _anfuService;


        public AFBillReceiptManagementController(ICommon commonsServices, IProcurementRetriever prService, IApplicationConfigurationRetriever acrService, IAccountNfinanceRetriever anfrService, TempUser currentUser, IAccountNfinanceUpsert anfuService)
        {
            _commonsServices = commonsServices;
            _prService = prService;
            _acrService = acrService;
            _anfrService = anfrService;
            _currentUser = currentUser;
            _anfuService = anfuService;
        }
        #region PORTION CONTAIN CODE TO: RENDER VIEW
        public IActionResult CreateUpdate_AFBillReceipt_BILLW_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            ViewBag.LocationId = _currentUser.BranchId; 
            return View();
        }
        public IActionResult CreateUpdate_AFBillReceipt_ACCW_UI(UISetting ui)
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
        public async Task<IActionResult> populateSupplierByParam(string operationType)
        {
            var result = await _prService.populateSupplierByParam(operationType, (int?)FilterConditions.PSupplier_Operation_ByCompany);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populatevPaymentMethodListByParam(string operationType)
        {
            var result = await _commonsServices.populatePaymentMethodByParam();
            return Json(result);
        }
        #endregion

        #region PORTION CONTAIN CODE TO: RETURN RECORD LIST
        [HttpGet]
        public async Task<IActionResult> populateBillListByParam(string operationType,Guid? guid, int?[] supplierIds, int?[] billStatusIds, DateTime? transactionDate)
        {
            var result = await _anfrService.populateBillByParam(operationType, guid, supplierIds, billStatusIds, transactionDate);
            return Json(new { data = result });
        }
        #endregion

        #region PORTION CONTAIN CODE TO: ADD/EDIT/DELETE DOCUMENT
        [HttpPost]
        public async Task<IActionResult> createUpdateBillReceipt([FromBody] PostedData postedData)
        {
            var result = await _anfuService.updateInsertDataInto_AFBillReceipt(postedData);
            return Json(new { result.IsSuccess, responseCode = result.StatusCode, message = result.Message });
        }
        #endregion
    }
}
