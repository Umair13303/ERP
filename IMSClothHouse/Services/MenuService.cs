using SharedUI.Interfaces;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.ViewModels;
using System.Net.Http.Headers;

namespace IMSClothHouse.Services
{
    public class MenuService : IMenuService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MenuService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<VMMenu>> getMenuForUserRole()
        {
            try
            {
                var userInfo = TempUser.Fill(_httpContextAccessor);
                if (!userInfo.IsAuthenticated) 
                    return new List<VMMenu>();
                var client = _httpClientFactory.CreateClient("GatewayClient");
                var token = _httpContextAccessor.HttpContext?.Request.Cookies["ERP_Auth_Token"];
                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                var url = $"{APIPaths.InternalDomain}/" +
                                          $"{nameof(ERPProject.OrganisationSetup)}/" +
                                          $"{nameof(SetupRoute.Area.ApplicationConfiguration)}/" +
                                          $"{nameof(SetupRoute.Api.COMInternalAPI)}/getMenuForUserRole" +
                                          $"?userId={userInfo.UserId}&roleId={userInfo.RoleId}";
                var response = await client.GetFromJsonAsync<List<VMMenu>>(url);

                return response ?? new List<VMMenu>();
            }
            catch (Exception ex)
            {
                return new List<VMMenu>();
            }
        }
    }
}