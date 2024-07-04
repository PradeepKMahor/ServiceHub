using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Controllers;
using ServiceHub.WebApp.Models;

namespace ServiceHub.WebApp.Areas.Masters.Controllers
{
    [Area("Masters")]
    public class UsersClientController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateClientUserIndex()
        {
            var userRegistrationCreateViewModel = new UserRegistrationCreateViewModel();
            return View(userRegistrationCreateViewModel);
        }
    }
}