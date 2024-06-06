using Microsoft.AspNetCore.Mvc;

namespace ServiceHub.WebApp.Areas.Masters.Controllers
{
    [Area("Masters")]
    public class ProducsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}