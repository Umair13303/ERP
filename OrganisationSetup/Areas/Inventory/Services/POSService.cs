using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Models.DAL.StoredProcedure;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.Responses;
using SharedUI.Models.SQLParameters;
using SharedUI.Models.TVP;
using SharedUI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganisationSetup.Areas.Inventory.Services
{
    public class POSSaleRequest
    {
        public int LocationId { get; set; }
        public string TransactionDate { get; set; }
        public List<POSSaleLineItem> LineItems { get; set; }
        public decimal TotalAmount { get; set; }
        public string Description { get; set; }
    }

    public class POSSaleLineItem
    {
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
    }

    public class POSProductDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public decimal SalePrice { get; set; }
        public decimal AvailableQuantity { get; set; }
        public int InventoryAccountId { get; set; }
    }

    public interface IPOSService
    {
        Task<List<POSProductDTO>> GetProductsWithStockAndPricing(int? locationId, int? companyId);
        Task<List<POSProductDTO>> SearchProducts(string searchTerm, int? locationId, int? companyId);
        Task<POSProductDTO> GetProductDetails(int productId, int? locationId);
        Task<ServiceResult> RecordPOSSale(POSSaleRequest posSaleRequest, TempUser currentUser);
        Task<ServiceResult> CreateInventoryAdjustment(PostedData postedData, TempUser currentUser);
    }

    public class POSService : IPOSService
    {
        private readonly ERPOrganisationSetupContext _context;
        private readonly IOSDataLayer _repo;
        private readonly IInventoryUpsert _upsertService;
        private readonly string _connectionString;

        public POSService(ERPOrganisationSetupContext context, IOSDataLayer repo, IInventoryUpsert upsertService)
        {
            _context = context;
            _repo = repo;
            _upsertService = upsertService;
            _connectionString = context.Database.GetDbConnection().ConnectionString;
        }

        public async Task<List<POSProductDTO>> GetProductsWithStockAndPricing(int? locationId, int? companyId)
        {
            try
            {
                var products = await _context.IProduct
                    .AsNoTracking()
                    .Where(p => p.CompanyId == companyId && p.Status == true)
                    .Select(p => new POSProductDTO
                    {
                        Id = p.Id,
                        Description = p.Description,
                        SKU = p.SKU,
                        SalePrice = _context.AFInvoicePPI
                            .Where(i => i.ProductId == p.Id && i.Status == true)
                            .OrderByDescending(i => i.CreatedOn)
                            .Select(i => i.Quantity > 0 ? i.ChargedAmount / (decimal)i.Quantity : 0)
                            .FirstOrDefault(),
                        AvailableQuantity = GetAvailableStock(p.Id, locationId),
                        InventoryAccountId = 1
                        //p.IProductATIs.FirstOrDefault().InventoryAccountId ?? 0
                    })
                    .ToListAsync();

                return products;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching products: {ex.Message}");
            }
        }

        public async Task<List<POSProductDTO>> SearchProducts(string searchTerm, int? locationId, int? companyId)
        {
            var products = await _context.IProduct
                .AsNoTracking()
                .Where(p => p.CompanyId == companyId && p.Status == true &&
                       (p.Description.Contains(searchTerm) || p.SKU.Contains(searchTerm)))
                .Select(p => new POSProductDTO
                {
                    Id = p.Id,
                    Description = p.Description,
                    SKU = p.SKU,
                    SalePrice = _context.AFInvoicePPI
                        .Where(i => i.ProductId == p.Id && i.Status == true)
                        .OrderByDescending(i => i.CreatedOn)
                        .Select(i => i.Quantity > 0 ? i.ChargedAmount / (decimal)i.Quantity : 0)
                        .FirstOrDefault(),
                    AvailableQuantity = GetAvailableStock(p.Id, locationId),
                    InventoryAccountId =0
                    //p.IProductATI.FirstOrDefault().InventoryAccountId ?? 0
                })
                .ToListAsync();

            return products;
        }

        public async Task<POSProductDTO> GetProductDetails(int productId, int? locationId)
        {
            var product = await _context.IProduct
                .AsNoTracking()
                .Where(p => p.Id == productId && p.Status == true)
                //.Include(p => p.IProductATIs)
                .Select(p => new POSProductDTO
                {
                    Id = p.Id,
                    Description = p.Description,
                    SKU = p.SKU,
                    SalePrice = _context.AFInvoicePPI
                        .Where(i => i.ProductId == p.Id && i.Status == true)
                        .OrderByDescending(i => i.CreatedOn)
                        .Select(i => i.Quantity > 0 ? i.ChargedAmount / (decimal)i.Quantity : 0)
                        .FirstOrDefault(),
                    AvailableQuantity = GetAvailableStock(p.Id, locationId),
                    InventoryAccountId =0
                    //p.IProductATIs.FirstOrDefault().InventoryAccountId ?? 0
                })
                .FirstOrDefaultAsync();

            return product;
        }

        public async Task<ServiceResult> RecordPOSSale(POSSaleRequest posSaleRequest, TempUser currentUser)
        {
            using var con = new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
            await con.OpenAsync();
            using var transaction = con.BeginTransaction();

            try
            {
                // Create invoice
                var invoiceLineItems = new List<AFInvoicePPI_TVP>();

                foreach (var item in posSaleRequest.LineItems)
                {
                    invoiceLineItems.Add(new AFInvoicePPI_TVP
                    {
                        Id = 0,
                        GuID = Guid.NewGuid(),
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        ActualAmount = item.Amount,
                        DiscountAmount = 0,
                        ChargedAmount = item.Amount,
                        CreatedOn = DateTime.Now,
                        CreatedBy = currentUser.UserId,
                        UpdatedOn = DateTime.Now,
                        UpdatedBy = currentUser.UserId,
                        DocumentType = (int?)DocumentType.invoiceProduct,
                        DocumentStatus = (int?)DocumentStatus.active,
                        Status = true
                    });
                }

                var invoiceResult = await _repo.UpsertInto_AFInvoice(
                    nameof(OperationType.INSERT_DATA_INTO_DB),
                    Guid.NewGuid(),
                    posSaleRequest.LocationId,
                    DateTime.Parse(posSaleRequest.TransactionDate),
                    1, // Assuming customer ID 1 for POS sales (cash customer)
                    posSaleRequest.Description ?? "POS Sale",
                    null,
                    posSaleRequest.TotalAmount,
                    (int?)InvoiceType.POSSale,
                    (int?)InvoiceStatus.paid,
                    DateTime.Now,
                    currentUser.UserId,
                    DateTime.Now,
                    currentUser.UserId,
                    (int?)DocumentType.invoice,
                    (int?)DocumentStatus.active,
                    currentUser.BranchId,
                    currentUser.CompanyId,
                    invoiceLineItems,
                    con, transaction);

                if (invoiceResult.response != (int)Code.Created && invoiceResult.response != (int)Code.Accepted)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult.failure("Failed to record POS sale", (int)Code.BadRequest);
                }

                // Create inventory adjustment (outbound)
                foreach (var item in posSaleRequest.LineItems)
                {
                    var adjData = new PostedData
                    {
                        OperationType = nameof(OperationType.INSERT_DATA_INTO_DB),
                        LocationId = posSaleRequest.LocationId,
                        TransactionDate = DateTime.Parse(posSaleRequest.TransactionDate),
                        Description = $"POS Sale - Invoice {invoiceResult.documentCode}",
                        ProductId = item.ProductId,
                        InventoryAdjustmentTypeId = (int?)AdjustmentType.Shortage,
                        UnitPurchasePrice = item.UnitPrice,
                        UnitSalePrice = item.UnitPrice,
                        QuantityIn = 0,
                        QuantityOut = item.Quantity
                    };

                    // You would call adjustment service here if needed
                }

                await transaction.CommitAsync();
                return ServiceResult.success($"POS Sale recorded successfully. Invoice: {invoiceResult.documentCode}", (int)Code.Created);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ServiceResult.failure($"Error: {ex.Message}", (int)Code.InternalServerError);
            }
        }

        public async Task<ServiceResult> CreateInventoryAdjustment(PostedData postedData, TempUser currentUser)
        {
            return await _upsertService.updateInsertDataInto_IInventoryAdjustment(postedData);
        }

        private decimal GetAvailableStock(int productId, int? locationId)
        {
            // Calculate available stock = Opening + QtyIn - QtyOut
            var stock = _context.IInventoryAdjustment
                .Where(a => a.ProductId == productId && a.Status == true)
                .AsEnumerable()
                .Sum(a => (a.QuantityIn - a.QuantityOut));

            return Math.Max(stock, 0);
        }
    }
}