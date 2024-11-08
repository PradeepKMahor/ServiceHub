using Microsoft.AspNetCore.Mvc;

namespace ServiceHub.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View(); //test
        }
    }
}