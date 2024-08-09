using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Controllers;
using ServiceHub.WebApp.Models;

namespace ServiceHub.WebApp.Areas.MobileApp.Controllers
{
    [Area("MobileApp")]
    public class CustomerController : BaseController
    {
        public IActionResult Index()
        {
            var cmrViewModel = new CMRViewModel();
            return View(cmrViewModel);
        }

        public IActionResult CMRDashboardIndex()
        {
            var cmrViewModel = new CMRViewModel();
            return View(cmrViewModel);
        }

        public IActionResult PMRDashboardIndex()
        {
            var pmrViewModel = new PMRViewModel();
            return View(pmrViewModel);
        }

        public IActionResult WMRDashboardIndex()
        {
            var cmrViewModel = new CMRViewModel();
            return View(cmrViewModel);
        }

        public IActionResult CMRIndex()
        {
            var cmrViewModel = new CMRViewModel();
            return View(cmrViewModel);
        }

        public IActionResult PMRIndex()
        {
            var cmrViewModel = new CMRViewModel();
            return View(cmrViewModel);
        }

        public IActionResult WMRIndex()
        {
            var cmrViewModel = new CMRViewModel();
            return View(cmrViewModel);
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