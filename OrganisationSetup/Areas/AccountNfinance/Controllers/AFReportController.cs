using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Services;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using Microsoft.EntityFrameworkCore;
namespace OrganisationSetup.Areas.AccountNfinance.Controllers
{
    [Authorize]
    [Area(nameof(SetupRoute.Area.AccountNfinance))]
    public class AFReportController : Controller
    {
        private readonly ERPOrganisationSetupContext _eRPOSContext;
        private readonly TempUser _currentUser;
        private readonly ICommon _commonServices;
        public AFReportController(ERPOrganisationSetupContext eRPOSContext, TempUser currentUser, ICommon commonServices)
        {
            _eRPOSContext = eRPOSContext;
            _currentUser = currentUser;
            _commonServices = commonServices;
        }

        public async Task<IActionResult> InvoiceRptThermal(Guid guID)
        {
            if (!_currentUser.IsAuthenticated) return Unauthorized();

            var invoice = await _eRPOSContext.AFInvoice
                .AsNoTracking()
                .Where(i => i.GuID == guID && i.CompanyId == _currentUser.CompanyId && i.BranchId == _currentUser.BranchId)
                .FirstOrDefaultAsync();

            if (invoice == null) return NotFound();

            var lines = await _eRPOSContext.AFInvoicePPI
                .AsNoTracking()
                .Where(p => p.InvoiceId == invoice.Id && p.Status == true && p.DocumentStatus == (int)SharedUI.Models.Enums.DocumentStatus.active)
                .OrderBy(p => p.Id)
                .ToListAsync();

            var modelLines = new List<LineVm>();
            foreach (var l in lines)
            {
                string productName = (await _eRPOSContext.IProduct.AsNoTracking().Where(p => p.Id == l.ProductId).Select(p => p.Description).FirstOrDefaultAsync()) ?? "Item";
                string attributeDisplay = null;
                if (l.ProductCombinationId != null)
                {
                    var combo = await _eRPOSContext.osvProductCombination.AsNoTracking().Where(c => c.Id == l.ProductCombinationId).Select(c => c.Attribute).FirstOrDefaultAsync();
                    attributeDisplay = combo;
                }
                decimal unitSale = l.UnitSalePrice;
                if (unitSale == 0)
                {
                    var pl = await _eRPOSContext.AFProductPriceLog
                        .AsNoTracking()
                        .Where(p => p.ProductId == l.ProductId
                                    && (l.ProductCombinationId == null || p.ProductCombinationId == l.ProductCombinationId)
                                    && p.Status == true
                                    && p.DocumentStatus == (int)SharedUI.Models.Enums.DocumentStatus.active
                                    && p.CompanyId == _currentUser.CompanyId
                                    && p.BranchId == _currentUser.BranchId)
                        .OrderByDescending(p => p.CreatedOn)
                        .Select(p => p.DefaultSalePrice)
                        .FirstOrDefaultAsync();
                    if (pl != 0) unitSale = pl;
                }

                modelLines.Add(new LineVm
                {
                    Description = productName,
                    Attribute = attributeDisplay,
                    Quantity = l.Quantity,
                    UnitSalePrice = unitSale,
                    ChargedAmount = l.ChargedAmount
                });
            }

            var vm = new InvoicePrintVm
            {
                Invoice = invoice,
                Lines = modelLines
            };

            return View(vm);
        }

        public class InvoicePrintVm
        {
            public AFInvoice Invoice { get; set; }
            public List<LineVm> Lines { get; set; } = new();
        }
        public class LineVm
        {
            public string Description { get; set; }
            public string? Attribute { get; set; }
            public decimal Quantity { get; set; }
            public decimal UnitSalePrice { get; set; }
            public decimal ChargedAmount { get; set; }
        }
    }

}
