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
        private readonly ERPOrganisationSetupContext _context;


        public AFInvoiceManagementController(ICommon commonsServices, ERPOrganisationSetupContext eRPOSC,ISaleOperationRetriever sorService, IApplicationConfigurationRetriever acrService, TempUser currentUser)
        {
            _commonsServices = commonsServices;
            _sorService = sorService;
            _acrService = acrService;
            _currentUser = currentUser;
            _context = eRPOSC;
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
        [HttpGet]
        public async Task<IActionResult> populateInvoiceListByParam(string operationType, int? customerId)
        {
            var query = from i in _context.AFInvoice
                        join c in _context.SOCustomer on i.CustomerId equals c.Id
                        join p in _context.AFInvoiceProduct on i.Id equals p.InvoiceId
                        where (!customerId.HasValue || i.CustomerId == customerId)
                        group p by new
                        {
                            i.Id,
                            i.Code,
                            i.TransactionDate,
                            i.DueAmount,
                            CustomerId = c.Id,
                            CustomerName = c.Description
                        } into g
                        select new
                        {
                            InvoiceId = g.Key.Id,
                            CustomerId = g.Key.CustomerId,
                            CustomerName = g.Key.CustomerName,
                            InvoiceNo = "INV-" + g.Key.Code,

                            // FIX 1: Handle Nullable DateTime
                            InvoiceDate = g.Key.TransactionDate.HasValue
                                          ? g.Key.TransactionDate.Value.ToString("yyyy-MM-dd")
                                          : "",

                            TotalAmount = g.Sum(x => x.ActualAmount),
                            TotalDiscount = g.Sum(x => x.DiscountAmount),

                            SaleTax = g.Sum(x => (x.ActualAmount - x.DiscountAmount) * 0.17m),
                            AdditionalTax = g.Sum(x => (x.ActualAmount - x.DiscountAmount) * 0.02m),

                            // Paid Amount calculation
                            PaidAmount = (g.Sum(x => x.ActualAmount) - g.Sum(x => x.DiscountAmount)) - g.Key.DueAmount
                        };

            var rawList = await query.ToListAsync();

            var result = rawList.Select(x => {
                // Use decimal type here to match the calculations above
                decimal net = x.TotalAmount - x.TotalDiscount + x.SaleTax + x.AdditionalTax;
                decimal balance = net - x.PaidAmount;

                return new
                {
                    x.CustomerId,
                    x.CustomerName,
                    x.InvoiceNo,
                    x.InvoiceDate,
                    x.TotalAmount,
                    x.SaleTax,
                    x.AdditionalTax,
                    GrossAmount = x.TotalAmount + x.SaleTax + x.AdditionalTax,
                    x.TotalDiscount,
                    NetAmount = net,
                    x.PaidAmount,
                    Status = balance <= 0 ? "Paid" : (x.PaidAmount > 0 ? "Partial" : "Pending")
                };
            }).ToList();

            return Json(result);
        }    
        #endregion
    }
}
