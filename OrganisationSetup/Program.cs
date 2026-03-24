using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrganisationSetup.Areas.AccountNfinance.Services;
using OrganisationSetup.Areas.ApplicationConfiguration.Services;
using OrganisationSetup.Areas.CompanySetup.Services;
using OrganisationSetup.Areas.Inventory.Services;
using OrganisationSetup.Areas.SaleOperation.Services;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Models.DAL.StoredProcedure;
using OrganisationSetup.Services;
using SharedUI.Filters;
using SharedUI.Interfaces;
using SharedUI.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ERPOrganisationSetupContext>(options =>    options.UseSqlServer(builder.Configuration.GetConnectionString("ERPOrganisationSetupConnection")));

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);
#region ADD REFERANCE FOR VIEWS
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<MenuFilter>();
})
.AddApplicationPart(typeof(SharedUI.Models.ViewModels.VMMenu).Assembly); builder.Services.AddHttpContextAccessor();
#endregion

#region ADD SERVICES
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["ERP_Auth_Token"];
            return Task.CompletedTask;
        }
    };
});
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IOSDataLayer, OSDataLayerRepository>();
builder.Services.AddScoped<ICommon, CommonServices>();
builder.Services.AddScoped<IApplicationConfigurationUpsert, ApplicationConfigurationUpsertService>();
builder.Services.AddScoped<IApplicationConfigurationValidation, ApplicationConfigurationValidationService>();
builder.Services.AddScoped<IApplicationConfigurationRetriever, ApplicationConfigurationRetrieverService>();

builder.Services.AddScoped<IAccountNfinanceUpsert, AccountNfinanceUpsertService>();
builder.Services.AddScoped<IAccountNfinanceValidation, AccountNfinanceValidationService>();
builder.Services.AddScoped<IAccountNfinanceRetriever, AccountNfinanceRetrieverService>();

builder.Services.AddScoped<ICompanySetupUpsert, CompanySetupUpsertService>();
builder.Services.AddScoped<ICompanySetupValidation, CompanySetupValidationService>();
builder.Services.AddScoped<ICompanySetupRetriever, CompanySetupRetriever>();

builder.Services.AddScoped<IInventoryUpsert, InventoryUpsertService>();
builder.Services.AddScoped<IInventoryValidation, InventoryValidationService>();
builder.Services.AddScoped<IInventoryRetriever, InventoryRetrieverService>();

builder.Services.AddScoped<ISaleOperationUpsert, SaleOperationUpsertService>();
builder.Services.AddScoped<ISaleOperationValidation, SaleOperationValidationService>();
builder.Services.AddScoped<ISaleOperationRetriever, SaleOperationRetrieverService>();


#endregion
var app = builder.Build();
var pathBase = builder.Configuration["PathBase"];
if (!string.IsNullOrEmpty(pathBase))
{
    app.UsePathBase(pathBase);

    app.Use((context, next) =>
    {
        context.Request.PathBase = pathBase;
        return next();
    });
}
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default_root",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=COMAuthentication}/{action=Login}/{id?}",
    defaults: new { area = "ApplicationConfiguration" });

app.Run();
