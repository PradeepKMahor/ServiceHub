using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Areas.Masters.Models;
using ServiceHub.WebApp.Areas.Users.Models.UserManagement;
using ServiceHub.WebApp.Controllers;
using ServiceHub.WebApp.Models;

namespace ServiceHub.WebApp.Areas.Users.Controllers
{
    [Area("Users")]
    public class UserManagementController : BaseController
    {
        public IActionResult Index()
        {
            var userViewModel = new UserViewModel();
            return View(userViewModel);
        }

        public IActionResult CreateUser()
        {
            var userCreateViewModel = new UserCreateViewModel();
            return View(userCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(UserCreateViewModel userCreateViewModel)
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

            return View(userCreateViewModel);
        }
    }
}