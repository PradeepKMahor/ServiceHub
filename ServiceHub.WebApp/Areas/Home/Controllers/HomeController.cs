using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceHub.WebApp.Areas.Home.Controllers
{
    [Area("Home")]
    public class HomeController : Controller
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