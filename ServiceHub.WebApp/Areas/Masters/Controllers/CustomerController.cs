using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Areas.Masters.Models;

namespace ServiceHub.WebApp.Areas.Masters.Controllers
{
    [Area("Masters")]
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            var customerViewModel = new CustomerViewModel();
            return View();
        }
    }
}