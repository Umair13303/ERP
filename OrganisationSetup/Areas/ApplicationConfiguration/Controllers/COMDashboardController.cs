using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedUI.Models.Enums;

namespace OrganisationSetup.Areas.ApplicationConfiguration.Controllers
{
    [Area(nameof(SetupRoute.Area.ApplicationConfiguration))]
    [Authorize]
    public class COMDashboardController : Controller
    {
        public IActionResult DashboardDefault()
        {
            return View();
        }
    }
}
