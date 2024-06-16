using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Controllers;

namespace ServiceHub.WebApp.Areas.Masters.Controllers
{
    [Area("Masters")]
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AssessesIndex()
        {
            return View();
        }

        public IActionResult ProductServicesIndex()
        {
            return View();
        }

        public IActionResult ServiceConfigIndex()
        {
            return View();
        }

        public IActionResult UsersCustomerIndex()
        {
            return View();
        }

        public IActionResult CustomerProductProfileIndex()
        {
            return View();
        }

        public IActionResult UsersClientIndex()
        {
            return View();
        }

        public IActionResult ProductIndex()
        {
            return View();
        }

        public IActionResult ServiceCauseIndex()
        {
            return View();
        }

        public IActionResult SOPsIndex()
        {
            return View();
        }
    }
}