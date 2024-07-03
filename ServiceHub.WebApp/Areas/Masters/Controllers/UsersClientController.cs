using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Areas.Masters.Models;
using ServiceHub.WebApp.Controllers;

namespace ServiceHub.WebApp.Areas.Masters.Controllers
{
    [Area("Masters")]
    public class UsersClientController : BaseController
    {
        public IActionResult Index()
        {
            var customerViewModel = new CustomerViewModel();
            return View();
        }
    }
}