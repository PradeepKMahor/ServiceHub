using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceHub.Domain.Context;
using ServiceHub.WebApp.Data;
using ServiceHub.WebApp.Interfaces;
using ServiceHub.WebApp.Repositories;
using WebEssentials.AspNetCore.Pwa;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DataContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddProgressiveWebApp(new PwaOptions
{
    RegisterServiceWorker = true,
    RegisterWebmanifest = false,  // (Manually register in Layout file)
    Strategy = ServiceWorkerStrategy.NetworkFirst,
    OfflineRoute = "Offline.html"
});
builder.Services.AddHealthChecks();
builder.Services.AddResponseCaching();

#region Session

#if DEBUG
builder.Services.AddDistributedMemoryCache();
#else
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = Configuration.GetConnectionString("RedisCache");
    options.InstanceName = "redis";
});

#endif
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

#endregion Session

//builder.Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
//              .AddRazorPagesOptions(options =>
//              {
//                  options.Conventions.AuthorizeAreaFolder("Identity", $"/Account/Manage");
//                  options.Conventions.AuthorizeAreaPage("Identity", $"/Account/Logout");
//              });
//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.LoginPath = $"/Identity/Account/Login";

//    options.LogoutPath = $"/Identity/Account/Logout";

//    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
//});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddTransient<ICategories, CategoriesRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

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
app.UseResponseCaching();
//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseCookiePolicy();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(name: "Identity", areaName: "Identity", pattern: "Identity/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute(name: "Admin", areaName: "Admin", pattern: "Admin/{controller=Admin}/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute(name: "Masters", areaName: "Masters", pattern: "Masters/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute(name: "SystemAdmin", areaName: "SystemAdmin", pattern: "SystemAdmin/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.Run();