using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Controllers;
using ServiceHub.WebApp.Models;

namespace ServiceHub.WebApp.Areas.Technician.Controllers
{
    [Area("Technician")]
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            var cmrViewModel = new CMRViewModel();
            return View(cmrViewModel);
            return View();
        }

        public IActionResult DashboardIndex()
        {
            return View();
        }

        public IActionResult AboutUsIndex()
        {
            return View();
        }

        public IActionResult ContactUsIndex()
        {
            return View();
        }
    }
}