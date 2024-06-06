using Microsoft.AspNetCore.Mvc;

namespace ServiceHub.WebApp.Areas.Masters.Controllers
{
    [Area("Masters")]
    public class HomeController : Controller
    {
        public IActionResult HomeIndex()
        {
            return View();
        }
    }
}