using IMSClothHouse.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SharedUI.Filters;
using SharedUI.Interfaces;
using SharedUI.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);
// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<MenuFilter>();
})
.AddApplicationPart(typeof(SharedUI.Models.ViewModels.VMMenu).Assembly); 
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("GatewayClient")
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
    });
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
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

app.Run();
