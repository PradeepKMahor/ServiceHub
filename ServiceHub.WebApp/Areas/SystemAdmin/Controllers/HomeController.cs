using Microsoft.AspNetCore.Mvc;

namespace ServiceHub.WebApp.Areas.SystemAdmin.Controllers
{
    [Area("SystemAdmin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}