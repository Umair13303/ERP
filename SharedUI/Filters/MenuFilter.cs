using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using SharedUI.Interfaces;
using SharedUI.Models.Contexts;
using SharedUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUI.Filters
{
    public class MenuFilter: IAsyncActionFilter
    {
        private readonly ISessionService _iSessionService;
        private readonly IMenuService _iMenuService;

        public MenuFilter(ISessionService iSessionService, IMenuService iMenuService)
        {
            _iSessionService = iSessionService;
            _iMenuService = iMenuService;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var accessor = context.HttpContext.RequestServices.GetRequiredService<IHttpContextAccessor>();
            var userInfo = TempUser.Fill(accessor);

            if (userInfo.IsAuthenticated)
            {
                if (_iSessionService.UserMenu == null || !_iSessionService.UserMenu.Any())
                {
                    var menu = await _iMenuService.getMenuForUserRole();
                    _iSessionService.UserMenu = menu;
                }
            }
            await next();
        }
    }
}
