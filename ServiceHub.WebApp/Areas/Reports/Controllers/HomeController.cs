using Microsoft.AspNetCore.Mvc;

namespace ServiceHub.WebApp.Areas.Reports.Controllers
{
    [Area("Reports")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}