using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Controllers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceHub.WebApp.Areas.Home.Controllers
{
    [Area("Home")]
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