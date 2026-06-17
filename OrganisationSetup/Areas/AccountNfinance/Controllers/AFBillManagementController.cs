using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganisationSetup.Areas.AccountNfinance.Services;
using OrganisationSetup.Areas.ApplicationConfiguration.Services;
using OrganisationSetup.Areas.Inventory.Services;
using OrganisationSetup.Areas.Procurement.Services;
using OrganisationSetup.Services;
using SharedUI.Models.Configurations;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;

namespace OrganisationSetup.Areas.AccountNfinance.Controllers
{
    [Authorize]
    [Area(nameof(SetupRoute.Area.AccountNfinance))]
    public class AFBillManagementController : Controller
    {
        private readonly ICommon _commonsServices;
        private readonly TempUser _currentUser;
        private readonly IApplicationConfigurationRetriever _acrService;
        private readonly IInventoryRetriever _irService;
        private readonly IProcurementRetriever _prService;
        private readonly IAccountNfinanceUpsert _anfuService;
        private readonly IAccountNfinanceRetriever _anfrService;



        public AFBillManagementController(ICommon commonsServices,TempUser currentUser, IApplicationConfigurationRetriever acrService, IInventoryRetriever irService, IProcurementRetriever prService, IAccountNfinanceUpsert anfuService, IAccountNfinanceRetriever anfrService)
        {
            _commonsServices = commonsServices;
            _currentUser = currentUser;
            _acrService = acrService;
            _irService = irService;
            _prService = prService;
            _anfuService = anfuService;
            _anfrService = anfrService;
        }
        #region PORTION CONTAIN CODE TO: RENDER VIEW

        public IActionResult CreateUpdate_AFBill_DIR_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            ViewBag.LocationId = _currentUser.BranchId;
            if(ui.OperationType == nameof(OperationType.MPO_LIST))
            {
                var documentStatusList = Enum.GetValues(typeof(DocumentStatus))
                        .Cast<DocumentStatus>()
                        .Select(e => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                        {
                            Value = ((int)e).ToString(),
                            Text = e.ToString().ToUpper()
                        }).ToList();
                var billStatusList = Enum.GetValues(typeof(BillStatus))
                        .Cast<BillStatus>()
                        .Select(e => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                        {
                            Value = ((int)e).ToString(),
                            Text = e.ToString().ToUpper()
                        }).ToList();
                documentStatusList.Insert(0, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = null,
                    Text = "All"
                });
                billStatusList.Insert(0, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = null,
                    Text = "All"
                });
                ViewBag.DocumentStatusList = documentStatusList;
                ViewBag.BillStatusList = billStatusList;
            }
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
        public async Task<IActionResult> populateSupplierListByParam(string operationType)
        {
            var result = await _prService.populateSupplierByParam(operationType, (int?)FilterConditions.PSupplier_Operation_ByCompany);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populateProductListByParam(string operationType, string searchParam)
        {
            var result = await _irService.populateProductByParam(operationType,(int?)FilterConditions.IProduct_Operation_ALLActive_ByCompany, searchParam);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populatevAttributeListByParam()
        {
            var result = await _commonsServices.populateAttributeByParam();
            return Json(result);
        }

        #endregion
        
        #region PORTION CONTAIN CODE TO: RETURN RECORD LIST
        [HttpGet]
        public async Task<IActionResult> populateBillMasterListBySearch(string operationType, Guid? guid, int?[] supplierIds, int?[] billStatusIds, DateTime? transactionDate)
        {
            var result = await _anfrService.populateBillByParam(operationType, guid, supplierIds, billStatusIds, transactionDate);
            return Json(new { data = result });
        }
        #endregion

        #region PORTION CONTAIN CODE TO: ADD/EDIT/DELETE DOCUMENT
        [HttpPost]
        public async Task<IActionResult> createUpdateBill([FromBody] PostedData postedData)
        {
            var result = await _anfuService.updateInsertDataInto_AFBill(postedData);
            return Json(new { result.IsSuccess, responseCode = result.StatusCode, message = result.Message });
        }
        #endregion

    }
}
