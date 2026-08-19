using Microsoft.AspNetCore.Mvc;

namespace OrganisationSetup.Areas.Reporting.Controllers
{
    public class SOReportUIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
