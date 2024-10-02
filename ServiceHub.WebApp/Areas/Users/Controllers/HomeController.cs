using Microsoft.AspNetCore.Mvc;

namespace ServiceHub.WebApp.Areas.Users.Controllers
{
    [Area("Users")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserRegistrationIndex()
        {
            var userRegistrationCreateViewModel = new UserRegistrationCreateViewModel();
            return View(userRegistrationCreateViewModel);
        }
    }
}