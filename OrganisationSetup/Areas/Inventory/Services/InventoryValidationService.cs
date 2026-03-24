using OrganisationSetup.Models.DAL;
using SharedUI.Models.Enums;
using Microsoft.EntityFrameworkCore; // Required for async methods

namespace OrganisationSetup.Areas.Inventory.Services
{
    public interface IInventoryValidation
    {
        Task<bool> isISectionValid(string? operationType, Guid? guID, int? departmentId, string? description);
        Task<bool> isICategoryValid(string? operationType, Guid? guID, int? sectionId, string? description);
        Task<bool> isISubCategoryValid(string? operationType, Guid? guID, int? categoryId, string? description);
        Task<bool> isIBrandValid(string? operationType, Guid? guID, string? description);
        Task<bool> isIProductValid(string? operationType, Guid? guID, string? description, string? machineNumber, string? sku);
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

    }
}