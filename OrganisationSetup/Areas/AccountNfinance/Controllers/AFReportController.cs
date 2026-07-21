using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Services;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using System.Data;

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

        public enum InvoiceRptType
        {
            ThermalPrint = 1,
            PaperSize = 2
        }

        public async Task<IActionResult> InvoiceRptThermal(Guid guID, int invoiceRptType = (int)InvoiceRptType.ThermalPrint)
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
                string productName = await _eRPOSContext.IProduct
                    .AsNoTracking()
                    .Where(p => p.Id == l.ProductId)
                    .Select(p => p.Description)
                    .FirstOrDefaultAsync() ?? "Item";

                string attributeDisplay = null;
                if (l.ProductCombinationId != null)
                {
                    attributeDisplay = await _eRPOSContext.osvProductCombination
                        .AsNoTracking()
                        .Where(c => c.Id == l.ProductCombinationId)
                        .Select(c => c.Attribute)
                        .FirstOrDefaultAsync();
                }

                decimal unitSale = l.UnitSalePrice;
                if (unitSale == 0m)
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

                    if (pl != 0m) unitSale = pl;
                }

                decimal discount = l.DiscountAmount;
                decimal recalculatedCharged = Math.Max(0m, (unitSale * l.Quantity) - discount);

                modelLines.Add(new LineVm
                {
                    Description = productName,
                    Attribute = attributeDisplay,
                    Quantity = l.Quantity,
                    UnitSalePrice = unitSale,
                    ChargedAmount = recalculatedCharged
                });
            }

            var vm = new InvoicePrintVm
            {
                Invoice = invoice,
                Lines = modelLines
            };

            // Branch on report type
            if (invoiceRptType == (int)InvoiceRptType.PaperSize)
            {
                vm.Totals = await GetHeaderTotalsAsync(invoice.Id);
                return View("InvoiceRptThermal", vm);
            }

            return View("InvoiceRptThermal", vm);
        }

        private async Task<HeaderTotalsVm> GetHeaderTotalsAsync(int invoiceId)
        {
            var totals = new HeaderTotalsVm();
            var conn = _eRPOSContext.Database.GetDbConnection();
            bool wasClosed = conn.State != ConnectionState.Open;
            if (wasClosed) await conn.OpenAsync();

            try
            {
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                    SELECT 
                        SUM(CALC.GrossAmount)         AS DocGrossAmount,
                        SUM(CALC.DiscountAmount)      AS DocDiscountAmount,
                        SUM(CALC.TaxableAmount)       AS DocTaxableAmount,
                        SUM(CALC.SaleTaxAmount)       AS DocSaleTaxAmount,
                        SUM(CALC.AdditionalTaxAmount) AS DocAdditionalTaxAmount,
                        SUM(CALC.NetAmount)           AS DocNetAmount
                    FROM AFInvoice AS I
                    CROSS APPLY dbo.fn_GetInvoiceCalculations(I.Id, 1) AS CALC
                    WHERE I.Id = @InvoiceId";

                var p = cmd.CreateParameter();
                p.ParameterName = "@InvoiceId";
                p.Value = invoiceId;
                cmd.Parameters.Add(p);

                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    totals.DocGrossAmount = reader["DocGrossAmount"] as decimal? ?? 0m;
                    totals.DocDiscountAmount = reader["DocDiscountAmount"] as decimal? ?? 0m;
                    totals.DocTaxableAmount = reader["DocTaxableAmount"] as decimal? ?? 0m;
                    totals.DocSaleTaxAmount = reader["DocSaleTaxAmount"] as decimal? ?? 0m;
                    totals.DocAdditionalTaxAmount = reader["DocAdditionalTaxAmount"] as decimal? ?? 0m;
                    totals.DocNetAmount = reader["DocNetAmount"] as decimal? ?? 0m;
                }
            }
            finally
            {
                if (wasClosed) await conn.CloseAsync();
            }

            return totals;
        }

        public class InvoicePrintVm
        {
            public AFInvoice Invoice { get; set; }
            public List<LineVm> Lines { get; set; } = new();
            public HeaderTotalsVm Totals { get; set; } = new();
        }

        public class LineVm
        {
            public string Description { get; set; }
            public string? Attribute { get; set; }
            public decimal Quantity { get; set; }
            public decimal UnitSalePrice { get; set; }
            public decimal ChargedAmount { get; set; }
        }

        public class HeaderTotalsVm
        {
            public decimal DocGrossAmount { get; set; }
            public decimal DocDiscountAmount { get; set; }
            public decimal DocTaxableAmount { get; set; }
            public decimal DocSaleTaxAmount { get; set; }
            public decimal DocAdditionalTaxAmount { get; set; }
            public decimal DocNetAmount { get; set; }
        }
    }
}