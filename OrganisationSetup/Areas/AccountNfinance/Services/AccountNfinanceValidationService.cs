using OrganisationSetup.Models.DAL;
using SharedUI.Models.Enums;
using Microsoft.EntityFrameworkCore; // Required for async methods

namespace OrganisationSetup.Areas.AccountNfinance.Services
{
    public interface IAccountNfinanceValidation
    {
        Task<bool> isAFChartOfAccountValid(string? operationType, Guid? guID, string? description);
    }

    public class AccountNfinanceValidationService : IAccountNfinanceValidation
    {
        private readonly ERPOrganisationSetupContext _eRPOSContext;

        public AccountNfinanceValidationService(ERPOrganisationSetupContext eRPOSC)
        {
            _eRPOSContext = eRPOSC;
        }

        public async Task<bool> isAFChartOfAccountValid(string? operationType, Guid? guID, string? description)
        {
            if (string.IsNullOrEmpty(operationType)) return false;
            switch (operationType)
            {
                case nameof(OperationType.INSERT_DATA_INTO_DB):
                    return !await _eRPOSContext.AFChartOfAccount
                        .AnyAsync(x => x.Description!.Trim().ToLower() == description!.Trim().ToLower());

                case nameof(OperationType.UPDATE_DATA_INTO_DB):
                    bool exists = await _eRPOSContext.AFChartOfAccount.AnyAsync(x => x.GuID == guID);

                    return exists;

                default:
                    return false;
            }
        }


    }
}