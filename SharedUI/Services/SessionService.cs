using Microsoft.AspNetCore.Http;
using SharedUI.Interfaces;
using SharedUI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SharedUI.Services
{
    public class SessionService:ISessionService
    {
        private readonly IHttpContextAccessor _accessor;
        public SessionService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        private ClaimsPrincipal? User => _accessor.HttpContext?.User;
        public int UserId => int.TryParse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id) ? id : 0;
        public string UserName => User?.Identity?.Name ?? "Guest";
        public string CompanyName => User?.FindFirst("CompanyName")?.Value ?? "ERP System";
        public string AllowedBranchIds => User?.FindFirst("AllowedBranchIds")?.Value ?? "0";
        public List<VMMenu> UserMenu { get; set; } = new List<VMMenu>();
    }
}
