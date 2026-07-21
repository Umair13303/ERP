using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Services;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using System.Data;
using Microsoft.Data.SqlClient;

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
            A4Print = 2
        }

        public async Task<IActionResult> InvoiceRptThermal(Guid guID, int invoiceRptType = (int)InvoiceRptType.ThermalPrint)
        {
            if (!_currentUser.IsAuthenticated) return Unauthorized();

            string activeStatus = ((int)DocumentStatus.active).ToString();

            var header = await GetInvoiceHeaderAsync(_currentUser.BranchId, guID);
            if (header == null) return NotFound();

            var lines = await GetInvoiceLinesAsync(guID, activeStatus);

            var vm = new InvoicePrintVm
            {
                Header = header,
                Lines = lines,
                ReportType = (InvoiceRptType)invoiceRptType
            };

            return View("InvoiceRptThermal", vm);
        }

        private async Task<List<LineVm>> GetInvoiceLinesAsync(Guid guID, string documentStatus)
        {
            var result = new List<LineVm>();
            var conn = _eRPOSContext.Database.GetDbConnection();
            bool wasClosed = conn.State != ConnectionState.Open;
            if (wasClosed) await conn.OpenAsync();

            try
            {
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AFInvoice_RptDetail_GBLParam";

                cmd.Parameters.Add(new SqlParameter("@GuID", guID));
                cmd.Parameters.Add(new SqlParameter("@DocumentStatus", documentStatus));

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    result.Add(new LineVm
                    {
                        Description = reader["ProductName"] as string ?? "Item",
                        Quantity = reader["Quantity"] as decimal? ?? 0m,
                        UnitSalePrice = reader["DefaultUnitPrice"] as decimal? ?? 0m,
                        ActualAmount = reader["ActualAmount"] as decimal? ?? 0m,
                        DiscountAmount = reader["DiscountAmount"] as decimal? ?? 0m,
                        ChargedAmount = reader["ChargedAmount"] as decimal? ?? 0m
                    });
                }
            }
            finally
            {
                if (wasClosed) await conn.CloseAsync();
            }

            return result;
        }

        private async Task<HeaderVm> GetInvoiceHeaderAsync(int? locationId, Guid guID)
        {
            HeaderVm header = null;
            var conn = _eRPOSContext.Database.GetDbConnection();
            bool wasClosed = conn.State != ConnectionState.Open;
            if (wasClosed) await conn.OpenAsync();

            try
            {
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AFInvoice_RptHeader_GBLParam";

                cmd.Parameters.Add(new SqlParameter("@LocationId", locationId ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@InvoiceGUID", guID));

                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    header = new HeaderVm
                    {
                        InvoiceId = reader["InvoiceId"] as int? ?? 0,
                        GuID = reader["GuID"] as Guid? ?? Guid.Empty,
                        Code = reader["Code"] as string,
                        Location = reader["Location"] as string,
                        TransactionDate = reader["TransactionDate"] as DateTime?,
                        Customer = reader["Customer"] as string,
                        Description = reader["Description"] as string,
                        FBRStamp = reader["FBRStamp"] as string,
                        DueAmount = reader["DueAmount"] as decimal? ?? 0m,
                        InvoiceStatus = reader["InvoiceStatus"] as int?,
                        CreatedOn = reader["CreatedOn"] as DateTime?,
                        DocumentStatus = reader["DocumentStatus"] as int?,
                        UserName = reader["UserName"] as string,
                        DocGrossAmount = reader["DocGrossAmount"] as decimal? ?? 0m,
                        DocDiscountAmount = reader["DocDiscountAmount"] as decimal? ?? 0m,
                        DocTaxableAmount = reader["DocTaxableAmount"] as decimal? ?? 0m,
                        DocSaleTaxAmount = reader["DocSaleTaxAmount"] as decimal? ?? 0m,
                        DocAdditionalTaxAmount = reader["DocAdditionalTaxAmount"] as decimal? ?? 0m,
                        DocNetAmount = reader["DocNetAmount"] as decimal? ?? 0m
                    };
                }
            }
            finally
            {
                if (wasClosed) await conn.CloseAsync();
            }

            return header;
        }

        public class InvoicePrintVm
        {
            public HeaderVm Header { get; set; }
            public List<LineVm> Lines { get; set; } = new();
            public InvoiceRptType ReportType { get; set; }
        }

        public class LineVm
        {
            public string Description { get; set; }
            public string Attribute { get; set; }
            public decimal Quantity { get; set; }
            public decimal UnitSalePrice { get; set; }
            public decimal ActualAmount { get; set; }
            public decimal DiscountAmount { get; set; }
            public decimal ChargedAmount { get; set; }
        }

        public class HeaderVm
        {
            public int InvoiceId { get; set; }
            public Guid GuID { get; set; }
            public string Code { get; set; }
            public string Location { get; set; }
            public DateTime? TransactionDate { get; set; }
            public string Customer { get; set; }
            public string Description { get; set; }
            public string FBRStamp { get; set; }
            public decimal DueAmount { get; set; }
            public int? InvoiceStatus { get; set; }
            public DateTime? CreatedOn { get; set; }
            public int? DocumentStatus { get; set; }
            public string UserName { get; set; }
            public decimal DocGrossAmount { get; set; }
            public decimal DocDiscountAmount { get; set; }
            public decimal DocTaxableAmount { get; set; }
            public decimal DocSaleTaxAmount { get; set; }
            public decimal DocAdditionalTaxAmount { get; set; }
            public decimal DocNetAmount { get; set; }
        }
    }
}