using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace OrganisationSetup.Areas.Reporting.Controllers
{
    public class SOReportController : ApiController
    {
        public JsonResult generateCustomerLedgerByParam(int customerId = -1)
        {
            return new JsonResult(customerId);
        }
    }
}
