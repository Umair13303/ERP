using Microsoft.EntityFrameworkCore;
using NuGet.ProjectModel;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Services;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.ViewModels;
using System;
using System.Linq;
using static SharedUI.Models.ViewModels.DTObject;

namespace OrganisationSetup.Areas.Procurement.Services
{
    public interface IProcurementRetriever
    {
    }
    public class ProcurementRetrieverService : IProcurementRetriever
    {
        private readonly TempUser _currentUser;
        private readonly ERPOrganisationSetupContext _eRPOSContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICommon _commonsServices;


        public ProcurementRetrieverService(TempUser currentUser,ERPOrganisationSetupContext eRPOSC, IHttpContextAccessor httpContextAccessor, ICommon commonsServices)
        {
            _currentUser = currentUser;
            _eRPOSContext = eRPOSC;
            _httpContextAccessor = httpContextAccessor;
            _commonsServices = commonsServices;

        }
     
    }

}