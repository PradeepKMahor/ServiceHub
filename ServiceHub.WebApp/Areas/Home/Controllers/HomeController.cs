using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Controllers;

namespace ServiceHub.WebApp.Areas.Home.Controllers
{
    [Area("Home")]
    [Authorize]
    public class HomeController : BaseController
    {
        public IActionResult Index()
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