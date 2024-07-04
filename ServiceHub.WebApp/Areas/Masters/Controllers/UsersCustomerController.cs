using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Controllers;
using ServiceHub.WebApp.Models;

namespace ServiceHub.WebApp.Areas.Masters.Controllers
{
    [Area("Masters")]
    public class UsersCustomerController : BaseController
    {
        public IActionResult Index()
        {
            var userCustomerViewModel = new UserCustomerViewModel();
            return View(userCustomerViewModel);
        }

        public IActionResult CreateCustomerUser()
        {
            var userCustomerCreateViewModel = new UserCustomerCreateViewModel();
            return View(userCustomerCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCustomerUser(UserCustomerCreateViewModel userCustomerCreateViewModel)
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

            return View(userCustomerCreateViewModel);
        }
    }
}