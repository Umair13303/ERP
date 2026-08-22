using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Models.DAL;
using SharedUI.Models.Enums;
using SharedUI.Models.TVP;

namespace OrganisationSetup.Services
{
    public interface IAFAtomicServices
    {

   
    }

    public class AFAtomicServices : IAFAtomicServices
    {
        private readonly ERPOrganisationSetupContext _eRPOSContext;

        public AFAtomicServices(ERPOrganisationSetupContext eRPOSContext)
        {
            _eRPOSContext = eRPOSContext;
        }

    }
}
