using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using System.IO;
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

// --- FIX A: FORWARDED HEADERS (Fixes the "Unsafe Attempt" / Protocol mismatch) ---
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

// --- FIX B: DATA PROTECTION (Fixes 401 on Free Tier) ---
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "keys")))
    .SetApplicationName("OrganisationSetup");

builder.Services.AddDbContext<ERPOrganisationSetupContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ERPOrganisationSetupConnection")));

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<MenuFilter>();
})
.AddApplicationPart(typeof(SharedUI.Models.ViewModels.VMMenu).Assembly);

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var token = context.Request.Cookies["ERP_Auth_Token"];
            if (!string.IsNullOrEmpty(token))
            {
                context.Token = token;
            }
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            context.Response.Headers.Append("Token-Error", context.Exception.Message);
            return Task.CompletedTask;
        }
    };
});

// Services (Keep your existing Scoped services here)
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

var app = builder.Build();

// PathBase
var pathBase = builder.Configuration["PathBase"];
if (!string.IsNullOrEmpty(pathBase))
{
    app.UsePathBase(pathBase);
    app.Use((context, next) => {
        context.Request.PathBase = pathBase;
        return next();
    });
}

// ORDER IS CRITICAL
app.UseForwardedHeaders(); // Must be first

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
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=COMAuthentication}/{action=Login}/{id?}",
    defaults: new { area = "ApplicationConfiguration" });

app.Run();