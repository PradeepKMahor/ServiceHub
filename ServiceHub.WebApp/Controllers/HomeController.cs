using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Models;
using System.Diagnostics;

namespace ServiceHub.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("CMRIndex", "Home");
            Notify("Success", "Data updated successfully", "toaster", NotificationType.success);
            return View();
        }

        public IActionResult CMRIndex()
        {
            if ((bool)HttpContext.Items["isMobile"])
            {
                return RedirectToAction("CMRDashboardIndex", "Home", new { Area = "MobileApp" });
            }
            Notify("Success", "Data updated successfully", "toaster", NotificationType.success);
            return View();
        }

        public IActionResult PMRIndex()
        {
            Notify("Success", "Data updated successfully", "toaster", NotificationType.success);
            return View();
        }

        public IActionResult WMRIndex()
        {
            Notify("Success", "Data updated successfully", "toaster", NotificationType.success);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}