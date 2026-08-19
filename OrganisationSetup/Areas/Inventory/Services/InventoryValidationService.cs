using Microsoft.EntityFrameworkCore; // Required for async methods
using OrganisationSetup.Models.DAL;
using SharedUI.Models.Enums;
using SharedUI.Models.Responses;
using SharedUI.Models.SQLParameters;
using SharedUI.Models.TVP;

namespace OrganisationSetup.Areas.Inventory.Services
{
    public interface IInventoryValidation
    {
        Task<bool> isISectionValid(string? operationType, Guid? guID, int? departmentId, string? description);
        Task<bool> isICategoryValid(string? operationType, Guid? guID, int? sectionId, string? description);
        Task<bool> isISubCategoryValid(string? operationType, Guid? guID, int? categoryId, string? description);
        Task<bool> isIBrandValid(string? operationType, Guid? guID, string? description);
        Task<bool> isIProductValid(string? operationType, Guid? guID, string? description, string? machineNumber, string? sku);
        Task<ServiceResult> isAdjustmentValid(string? operationType, Guid? GuID, int? companyId, int? locationId, int? adjustmentTypeId, List<IInventoryAdjustmentPPQD_TVP> inventoryAdjustmentPPQD_TVP);
    }

    public class InventoryValidationService : IInventoryValidation
    {
        private readonly ERPOrganisationSetupContext _eRPOSContext;

        public InventoryValidationService(ERPOrganisationSetupContext eRPOSC)
        {
            _eRPOSContext = eRPOSC;
        }

        public async Task<bool> isISectionValid(string? operationType, Guid? guID, int? departmentId, string? description)
        {
            if (string.IsNullOrEmpty(operationType)) return false;
            switch (operationType)
            {
                case nameof(OperationType.INSERT_DATA_INTO_DB):
                    return !await _eRPOSContext.ISection
                        .AnyAsync(x => x.Description!.Trim().ToLower() == description!.Trim().ToLower());

                case nameof(OperationType.UPDATE_DATA_INTO_DB):
                    bool exists = await _eRPOSContext.ISection.AnyAsync(x => x.GuID == guID);
                    return exists;
                default:
                    return false;
            }
        }
        public async Task<bool> isICategoryValid(string? operationType, Guid? guID, int? sectionId, string? description)
        {
            if (string.IsNullOrEmpty(operationType)) return false;
            switch (operationType)
            {
                case nameof(OperationType.INSERT_DATA_INTO_DB):
                    return !await _eRPOSContext.ICategory
                        .AnyAsync(x => x.Description!.Trim().ToLower() == description!.Trim().ToLower() && x.SectionId == sectionId);

                case nameof(OperationType.UPDATE_DATA_INTO_DB):
                    bool exists = await _eRPOSContext.ICategory.AnyAsync(x => x.GuID == guID);
                    return exists;
                default:
                    return false;
            }
        }
        public async Task<bool> isISubCategoryValid(string? operationType, Guid? guID, int? categoryId, string? description)
        {
            if (string.IsNullOrEmpty(operationType)) return false;
            switch (operationType)
            {
                case nameof(OperationType.INSERT_DATA_INTO_DB):
                    return !await _eRPOSContext.ISubCategory
                        .AnyAsync(x => x.Description!.Trim().ToLower() == description!.Trim().ToLower() && x.CategoryId == categoryId);

                case nameof(OperationType.UPDATE_DATA_INTO_DB):
                    bool exists = await _eRPOSContext.ISubCategory.AnyAsync(x => x.GuID == guID);
                    return exists;
                default:
                    return false;
            }
        }
        public async Task<bool> isIBrandValid(string? operationType, Guid? guID, string? description)
        {
            if (string.IsNullOrEmpty(operationType)) return false;
            switch (operationType)
            {
                case nameof(OperationType.INSERT_DATA_INTO_DB):
                    return !await _eRPOSContext.IBrand
                        .AnyAsync(x => x.Description!.Trim().ToLower() == description!.Trim().ToLower());

                case nameof(OperationType.UPDATE_DATA_INTO_DB):
                    bool exists = await _eRPOSContext.IBrand.AnyAsync(x => x.GuID == guID);
                    return exists;
                default:
                    return false;
            }
        }
        public async Task<bool> isIProductValid(string? operationType, Guid? guID, string? description, string? machineNumber, string? sku)
        {
            if (string.IsNullOrEmpty(operationType)) return false;
            switch (operationType)
            {
                case nameof(OperationType.INSERT_DATA_INTO_DB):
                    return !await _eRPOSContext.IProduct
                        .AnyAsync(x => x.Description!.Trim().ToLower() == description!.Trim().ToLower() && x.MachineNumber!.Trim().ToLower() == machineNumber!.Trim().ToLower() && x.SKU!.Trim().ToLower() == sku!.Trim().ToLower());

                case nameof(OperationType.UPDATE_DATA_INTO_DB):
                    bool exists = await _eRPOSContext.IProduct.AnyAsync(x => x.GuID == guID);
                    return exists;
                default:
                    return false;
            }
        }
        public async Task<ServiceResult> isAdjustmentValid(string? operationType,Guid? GuID, int? companyId, int? locationId, int? adjustmentTypeId, List<IInventoryAdjustmentPPQD_TVP> inventoryAdjustmentPPQD_TVP)
        {
            if(operationType == nameof(OperationType.UPDATE_DATA_INTO_DB))
            {
                if (GuID == null || GuID == Guid.Empty)
                    return ServiceResult.failure("Document identifier is required for update.", (int)Code.BadRequest);

                var existingDoc = await _eRPOSContext.IAdjustment
                    .Where(x => x.GuID == GuID)
                    .FirstOrDefaultAsync();

                if (existingDoc == null)
                    return ServiceResult.failure("Document not found.", (int)Code.NotFound);

                if (existingDoc.CompanyId != companyId)
                    return ServiceResult.failure("Document does not belong to your company.", (int)Code.Forbidden);

                if (existingDoc.LocationId != locationId)
                    return ServiceResult.failure("Document does not belong to your branch.", (int)Code.Forbidden);

                if (existingDoc.DocumentStatus == (int)DocumentStatus.deleted)
                    return ServiceResult.failure("Document has been deleted and cannot be edited.", (int)Code.BadRequest);

                if (existingDoc.AdjustmentStatus != (int)Default.adjustmentStatus)
                    return ServiceResult.failure("Document is posted/locked and cannot be edited.", (int)Code.BadRequest);

            }
            if (inventoryAdjustmentPPQD_TVP == null || inventoryAdjustmentPPQD_TVP.Count == 0)
            {
                return ServiceResult.failure("At least one line item is required.", (int)Code.BadRequest);
            }
            var adjustmentType = await _eRPOSContext.vInventoryAdjustmentType.Where(x => x.Id == adjustmentTypeId).FirstOrDefaultAsync();

            if (adjustmentType == null)
            {
                return ServiceResult.failure("Invalid adjustment type.", (int)Code.BadRequest);
            }
            var isDuplicate = inventoryAdjustmentPPQD_TVP.GroupBy(x => new
            {
                x.ProductId,
                AttrKey = CSharedUtility.attributeKeyBuilder(x.Attribute)
            }).FirstOrDefault(x => x.Count() > 1);

            if (isDuplicate != null)
            {
                return ServiceResult.failure($"Duplicate product detected (Product ID: {isDuplicate.Key.ProductId}). Please merge or remove duplicate rows.", (int)Code.BadRequest);
            }

            foreach (var item in inventoryAdjustmentPPQD_TVP)
            {
                if ((item.QuantityIn > 0 || item.QuantityOut > 0) && ((decimal?)item.UnitPurchasePrice ?? 0) <= 0)
                    return ServiceResult.failure("A valid unit cost is required for any stock movement (in or out).", (int)Code.BadRequest);

                if (item.QuantityIn > 0 && item.QuantityOut > 0)
                    return ServiceResult.failure("A single line cannot have both Quantity In and Quantity Out.", (int)Code.BadRequest);

                if (item.QuantityIn > 0 && !adjustmentType.IsQuantityIn)
                    return ServiceResult.failure($"Adjustment type '{adjustmentType.Description}' does not permit stock-in quantities.", (int)Code.BadRequest);

                if (item.QuantityOut > 0 && !adjustmentType.IsQuantityOut)
                    return ServiceResult.failure($"Adjustment type '{adjustmentType.Description}' does not permit stock-out quantities.", (int)Code.BadRequest);

                if (item.UnitPurchasePrice > 0 && !adjustmentType.IsPurchasePrice)
                    return ServiceResult.failure($"Adjustment type '{adjustmentType.Description}' does not permit purchase price entry.", (int)Code.BadRequest);

                if (item.UnitSalePrice > 0 && !adjustmentType.IsSalePrice)
                    return ServiceResult.failure($"Adjustment type '{adjustmentType.Description}' does not permit sale price entry.", (int)Code.BadRequest);
                if (adjustmentType.IsQuantityIn && item.QuantityIn > 0 && item.UnitPurchasePrice <= 0)
                    return ServiceResult.failure("Purchase price must be greater than zero for stock receipts.", (int)Code.BadRequest);
            }
            var adjustmentProductIds = inventoryAdjustmentPPQD_TVP.Select(x => x.ProductId).Distinct().ToList();
            var validAdjustmentProducts = await _eRPOSContext.IProduct.Where(x => adjustmentProductIds.Contains(x.Id) && x.CompanyId == companyId && x.Status == true && x.DocumentStatus == (int)DocumentStatus.active).ToListAsync();
            var validProductMapping = validAdjustmentProducts.ToDictionary(p => p.Id);

            foreach (var productId in adjustmentProductIds)
            {
                if (!validProductMapping.ContainsKey((int)productId))
                    return ServiceResult.failure($"Product {productId} no longer exists, is inactive, or belongs to another company.", (int)Code.BadRequest);
            }
            foreach (var line in inventoryAdjustmentPPQD_TVP)
            {
                var product = validProductMapping[(int)line.ProductId];
                if (product.IsExpiryApplicable == true)
                {
                    if (string.IsNullOrWhiteSpace(line.Batch) || line.ExpiryDate == null)
                        return ServiceResult.failure($"Batch and Expiry are required for '{product.Description}'.", (int)Code.BadRequest);
                }
            }
            return ServiceResult.success("Adjustment is valid.", (int)Code.OK);
        }

    }
}