using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedUI.Interfaces;
using SharedUI.Models.Enums;

namespace OrganisationSetup.Areas.ApplicationConfiguration.Controllers
{
    [Authorize]
    [Area(nameof(SetupRoute.Area.ApplicationConfiguration))]
    [Route(nameof(SetupRoute.Area.ApplicationConfiguration) + "/" + nameof(SetupRoute.Api.COMInternalAPI))]
    public class COMInternalAPIController : Controller
    {
        private readonly IMenuService _iMenuService;

        public COMInternalAPIController(IMenuService iMenuService)
        {
            _iMenuService = iMenuService;
        }
        [HttpGet("getMenuForUserRole")]
        public async Task<IActionResult> getMenuForUserRole(int userId, string roleId)
        {
            var menu = await _iMenuService.getMenuForUserRole();
            return Ok(menu);
        }
    }

}
