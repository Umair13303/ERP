using Microsoft.AspNetCore.Http;
using SharedUI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SharedUI.Models.Contexts
{
    public class TempUser
    {
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public string? RoleId { get; set; }
        public int? BranchId { get; set; }
        public int? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? AllowedBranchIds { get; set; }

        public bool IsAuthenticated { get; set; }
        public List<VMMenu> UserMenu { get; set; } = new List<VMMenu>();
        public static TempUser Fill(IHttpContextAccessor accessor)
        {
            var user = accessor.HttpContext?.User;

            if (user == null || !user.Identity.IsAuthenticated)
                return new TempUser { IsAuthenticated = false };

            return new TempUser
            {
                IsAuthenticated = true,
                UserId = int.TryParse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id) ? id : 0,
                UserName = user.Identity?.Name,
                RoleId = user.FindFirst("RoleId")?.Value,
                BranchId = int.TryParse(user.FindFirst("BranchId")?.Value, out var bId) ? bId : 0,
                CompanyId = int.TryParse(user.FindFirst("CompanyId")?.Value, out var cId) ? cId : 0,
                CompanyName = user.FindFirst("CompanyName")?.Value,
                AllowedBranchIds= user.FindFirst("AllowedBranchIds")?.Value
            };
        }
    }
}
