using DeviceDetectorNET.Cache;
using DeviceDetectorNET;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceHub.Domain.Context;
using ServiceHub.WebApp.Data;
using ServiceHub.WebApp.Interfaces;
using ServiceHub.WebApp.Repositories;
using WebEssentials.AspNetCore.Pwa;
using DeviceDetectorNET.Parser.Device;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DataContext>();

//Entity Framework
//builder.Services.AddDbContext<DataContext>(options =>
//   options.UseSqlServer(connectionString));

//Authentication and Authorization
//builder.Services.AddIdentity<IdentityUser, IdentityRole>() //Changes
//          .AddEntityFrameworkStores<DataContext>()
//          .AddDefaultTokenProviders();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddControllersWithViews();

builder.Services.AddProgressiveWebApp(new PwaOptions
{
    RegisterServiceWorker = true,
    RegisterWebmanifest = false,  // (Manually register in Layout file)
    Strategy = ServiceWorkerStrategy.Minimal,
    OfflineRoute = "Offline.html"
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddHealthChecks();
builder.Services.AddResponseCaching();

//#region Session

//#if DEBUG
//builder.Services.AddDistributedMemoryCache();
//#else
//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.Configuration = Configuration.GetConnectionString("RedisCache");
//    options.InstanceName = "redis";
//});

//#endif
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

//#endregion Session

builder.Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
              .AddRazorPagesOptions(options =>
              {
                  options.Conventions.AuthorizeAreaFolder("Identity", $"/Account/Manage");
                  options.Conventions.AuthorizeAreaPage("Identity", $"/Account/Logout");
              });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";

    options.LogoutPath = $"/Identity/Account/Logout";

    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

builder.Services.AddMvc().AddRazorPagesOptions(options =>
{
    options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "/Account/Login");
}).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddTransient<ICategories, CategoriesRepository>();
//builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    var detector = new DeviceDetector(context.Request.Headers["User-Agent"].ToString());
    detector.SetCache(new DictionaryCache());
    detector.Parse();

    if (detector.IsMobile())
    {
        context.Items.Remove("isMobile");
        context.Items.Add("isMobile", true);
    }
    else
    {
        context.Items.Remove("isMobile");
        context.Items.Add("isMobile", false);
    }

    context.Items.Remove("DeviceName");
    context.Items.Add("DeviceName", detector.GetDeviceName());

    await next();
});
app.UseResponseCaching();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseCookiePolicy();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(name: "Admin", areaName: "Admin", pattern: "Admin/{controller=Admin}/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute(name: "Reports", areaName: "Reports", pattern: "Reports/{controller=Admin}/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute(name: "Masters", areaName: "Masters", pattern: "Masters/{controller=Masters}/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute(name: "SystemAdmin", areaName: "SystemAdmin", pattern: "SystemAdmin/{controller=SystemAdmin}/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute(name: "Users", areaName: "Users", pattern: "Users/{controller=Users}/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute(name: "ServiceRequests", areaName: "ServiceRequests", pattern: "ServiceRequests/{controller=ServiceRequests}/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute(name: "MobileApp", areaName: "MobileApp", pattern: "MobileApp/{controller=MobileApp}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(name: "Home", pattern: "{controller=Home}/{action=CMRIndex}/{id?}");
    endpoints.MapAreaControllerRoute(name: "default", areaName: "Identity", pattern: "Identity/{action=Login}/{id?}");
    endpoints.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );
    endpoints.MapGet("/", context =>
    {
        return Task.Run(() => context.Response.Redirect("/Account/Login"));
    });
    endpoints.MapRazorPages();
});
app.Run();