using OrganisationSetup.Models.DAL;
using SharedUI.Models.Enums;
using Microsoft.EntityFrameworkCore; // Required for async methods

namespace OrganisationSetup.Areas.CompanySetup.Services
{
    public interface ICompanySetupValidation
    {
        Task<bool> isCSDepartmentValid(string? operationType, Guid? guID, string? description);


    }

    public class CompanySetupValidationService : ICompanySetupValidation
    {
        private readonly ERPOrganisationSetupContext _eRPOSContext;

        public CompanySetupValidationService(ERPOrganisationSetupContext eRPOSC)
        {
            _eRPOSContext = eRPOSC;
        }

        public async Task<bool> isCSDepartmentValid(string? operationType, Guid? guID, string? description)
        {
            if (string.IsNullOrEmpty(operationType)) return false;
            switch (operationType)
            {
                case nameof(OperationType.INSERT_DATA_INTO_DB):
                    return !await _eRPOSContext.CSDepartment
                        .AnyAsync(x => x.Description!.Trim().ToLower() == description!.Trim().ToLower());

                case nameof(OperationType.UPDATE_DATA_INTO_DB):
                    bool exists = await _eRPOSContext.CSDepartment.AnyAsync(x => x.GuID == guID);
                    return exists;
                default:
                    return false;
            }
        }

    }
}