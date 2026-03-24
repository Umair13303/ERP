using OrganisationSetup.Models.DAL;
using SharedUI.Models.Enums;
using Microsoft.EntityFrameworkCore; // Required for async methods

namespace OrganisationSetup.Areas.SaleOperation.Services
{
    public interface ISaleOperationValidation
    {
        Task<bool> isOSCustomerValid(string? operationType, Guid? guID,string? description);

    }

    public class SaleOperationValidationService : ISaleOperationValidation
    {
        private readonly ERPOrganisationSetupContext _eRPOSContext;

        public SaleOperationValidationService(ERPOrganisationSetupContext eRPOSC)
        {
            _eRPOSContext = eRPOSC;
        }
        public async Task<bool> isOSCustomerValid(string? operationType, Guid? guID, string? description)
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


    }
}