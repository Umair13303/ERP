using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Models.DAL;
using SharedUI.Interfaces;
using SharedUI.Models.Contexts;
using SharedUI.Models.ViewModels;

namespace OrganisationSetup.Services
{
    public class MenuService:IMenuService
    {
        private readonly ERPOrganisationSetupContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MenuService(ERPOrganisationSetupContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<VMMenu>> getMenuForUserRole()
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);
            if (!userInfo.IsAuthenticated || string.IsNullOrEmpty(userInfo.RoleId))
            {
                return new List<VMMenu>();
            }
            var rightList = await _context.vRight.AsNoTracking().ToListAsync();
            return rightList
                .Where(r => !string.IsNullOrEmpty(r.RoleIds) &&
                            r.RoleIds.Split(',').Contains(userInfo.RoleId) &&
                            r.Status == true && r.IsDisplayAllowed == true
                            )
                .GroupBy(r => r.Menu)
                .Select(menuGroup => new VMMenu
                {
                    Menu = menuGroup.Key,
                    SubMenu = menuGroup.GroupBy(r => r.SubMenu).Select(subMenuGroup => new VMSubMenu
                    {
                        SubMenu = subMenuGroup.Key,
                        Rights = subMenuGroup.Select(r => new VMRight
                        {
                            DisplayName = r.DisplayName,
                            Url = r.FormName,
                            OperationType =r.Purpose
                        }).ToList()
                    }).ToList()
                }).ToList();
        }
    }
}
