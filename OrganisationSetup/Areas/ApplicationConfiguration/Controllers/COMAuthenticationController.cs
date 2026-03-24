using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrganisationSetup.Models.DAL;
using SharedUI.Interfaces;
using SharedUI.Models.Enums;
using SharedUI.Models.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrganisationSetup.Areas.ApplicationConfiguration.Controllers
{
    [Area(nameof(SetupRoute.Area.ApplicationConfiguration))]
    public class COMAuthenticationController : Controller
    {
        private readonly ERPOrganisationSetupContext _eRPOSContext;
        private readonly ISessionService _iSessionService;
        private readonly IConfiguration _configuration;
        public COMAuthenticationController(ERPOrganisationSetupContext eRPOSC, ISessionService iSessionService, IConfiguration configuration)
        {
            _eRPOSContext = eRPOSC;
            _iSessionService = iSessionService;
            _configuration = configuration;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("ERP_Auth_Token", new CookieOptions
            {
                Path = "/",
                HttpOnly = true,
                Secure = true
            });

            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }

        #region PORTION CONTAIN CODE FOR : DATABASE OPERATION
        [HttpPost]
        public async Task<IActionResult> ValidateCredentials(ACUser postedData)
        {
            if (string.IsNullOrEmpty(postedData.Description) || string.IsNullOrEmpty(postedData.Password))
            {
                ModelState.AddModelError(string.Empty, SharedUI.Models.Responses.Message.serverResponse((int?)Code.NotFound));
                return View(nameof(Login), postedData);
            }
            var user = await _eRPOSContext.ACUser.FirstOrDefaultAsync(u => u.Description == postedData.Description && u.Password == postedData.Password);
            if (user != null)
            {
                var company = await _eRPOSContext.ACCompany.FirstOrDefaultAsync(c => c.Id == user.CompanyId);
                var branch = await _eRPOSContext.ACBranch.FirstOrDefaultAsync(b => b.Id == user.BranchId);
                #region IN CASE: USER LOGIN SUCCESS -- GET RIGHTS
                if (user != null)
                {

                    var token = GenerateJwtToken(user, company?.Description);
                    Response.Cookies.Append("ERP_Auth_Token", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Lax,
                        Expires = DateTime.UtcNow.AddMinutes(120),
                        Path = "/"
                    });

                    return RedirectToAction(nameof(SetupRoute.Action.DashboardDefault), nameof(SetupRoute.Controller.COMDashboard), new { area = nameof(SetupRoute.Area.ApplicationConfiguration) });
                }
                #endregion
            }
            ModelState.AddModelError(string.Empty, SharedUI.Models.Responses.Message.serverResponse((int?)Code.BadRequest));
            return View(nameof(Login), postedData);
        }
        #endregion
        private string GenerateJwtToken(ACUser user, string? companyName)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Description ?? "User"),
                new Claim("RoleId", user.RoleId.ToString()),
                new Claim("BranchId", user.BranchId.ToString()),
                new Claim("CompanyId", user.CompanyId.ToString()),
                new Claim("CompanyName", companyName ?? ""),
                new Claim("AllowedBranchIds", user.AllowedBranchIds.ToString() ?? "")

            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(120),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
