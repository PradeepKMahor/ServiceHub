using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.Domain;

namespace ServiceHub.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Administrator)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); //test
        }
    }
}