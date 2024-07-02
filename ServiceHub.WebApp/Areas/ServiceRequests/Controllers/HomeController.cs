using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Areas.Masters.Models;
using ServiceHub.WebApp.Areas.ServiceRequests.Models;
using ServiceHub.WebApp.Controllers;

namespace ServiceHub.WebApp.Areas.ServiceRequests.Controllers
{
    [Area("ServiceRequests")]
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}