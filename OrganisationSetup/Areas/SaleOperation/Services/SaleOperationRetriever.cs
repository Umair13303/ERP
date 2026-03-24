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

namespace OrganisationSetup.Areas.SaleOperation.Services
{
    public interface ISaleOperationRetriever
    {
    }
    public class SaleOperationRetrieverService : ISaleOperationRetriever
    {
        private readonly ERPOrganisationSetupContext _eRPOSContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICommon _commonsServices;


        public SaleOperationRetrieverService(ERPOrganisationSetupContext eRPOSC, IHttpContextAccessor httpContextAccessor, ICommon commonsServices)
        {
            _eRPOSContext = eRPOSC;
            _httpContextAccessor = httpContextAccessor;
            _commonsServices = commonsServices;

        }
    }

}
