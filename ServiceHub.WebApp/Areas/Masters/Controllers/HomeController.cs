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
    }
}