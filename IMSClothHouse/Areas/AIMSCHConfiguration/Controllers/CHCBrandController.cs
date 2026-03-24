using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedUI.Models.Enums;

namespace IMSClothHouse.Areas.AIMSCHConfiguration.Controllers
{
    [Area(nameof(SetupRoute.Area.AIMSCHConfiguration))]
    [Authorize]
    public class CHCBrandController : Controller
    {
        public IActionResult CreateUpdate_CHCBrand_UI(string DisplayName,string OperationType)
        {
            ViewBag.DisplayName = DisplayName.ToString().ToUpper();
            ViewBag.OperationType = OperationType.ToString().ToUpper();
            return View();
        }
    }
}
    