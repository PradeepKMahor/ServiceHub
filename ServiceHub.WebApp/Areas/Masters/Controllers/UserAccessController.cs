using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Models;
using ServiceHub.WebApp.Areas.Masters.Models;
using ServiceHub.WebApp.Controllers;

namespace ServiceHub.WebApp.Areas.Masters.Controllers
{
    [Area("Masters")]
    public class UserAccessController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateUser()
        {
            var user = new UserAccessCreateModel();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(UserAccessCreateModel userAccessCreateModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Notify("Success", "Data saved successfully", "toaster", notificationType: NotificationType.success);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Notify("Error", "Something Missing Or Data Not Found", "toaster", notificationType: NotificationType.error);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                Notify("Error", ex.Message, "toaster", notificationType: NotificationType.error);
            }

            return View(userAccessCreateModel);
        }
    }
}