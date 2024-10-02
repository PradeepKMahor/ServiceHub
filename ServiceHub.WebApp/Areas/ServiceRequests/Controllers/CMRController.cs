using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Controllers;
using ServiceHub.WebApp.Models;

namespace ServiceHub.WebApp.Areas.ServiceRequests.Controllers
{
    [Area("ServiceRequests")]
    public class CMRController : BaseController
    {
        public IActionResult Index()
        {
            //Notify("Success", "Success", "toaster", notificationType: NotificationType.success);
            var cmrViewModel = new CMRViewModel();
            return View(cmrViewModel);
        }

        public async Task<IActionResult> CreateCMR()
        {
            CMRCreateViewModel cmrCreateViewModel = new();

            return View(cmrCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCMR([FromForm] CMRCreateViewModel cmrCreateViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Notify("Success", "Data updated successfully", "toaster", NotificationType.success);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Notify("Error", "Something Missing Or Data Not Found", "toaster", NotificationType.error);
                    return View(cmrCreateViewModel);
                }
            }
            catch (Exception ex)
            {
                Notify("Exception", ex.Message + " " + ex.InnerException?.Message, "toaster", NotificationType.error);
            }
            return View(cmrCreateViewModel);
        }
    }
}