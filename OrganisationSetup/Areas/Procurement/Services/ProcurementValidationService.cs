using OrganisationSetup.Models.DAL;
using SharedUI.Models.Enums;
using Microsoft.EntityFrameworkCore; // Required for async methods

namespace OrganisationSetup.Areas.Procurement.Services
{
    public interface IProcurementValidation
    {

        public class ProcurementValidationService : IProcurementValidation
        {
            private readonly ERPOrganisationSetupContext _eRPOSContext;

            public ProcurementValidationService(ERPOrganisationSetupContext eRPOSC)
            {
                _eRPOSContext = eRPOSC;
            }
        }
    }
}