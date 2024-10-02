using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Controllers;
using ServiceHub.WebApp.Models;

namespace ServiceHub.WebApp.Areas.ServiceRequests.Controllers
{
    [Area("ServiceRequests")]
    public class WMRController : BaseController
    {
        public IActionResult Index()
        {
            //Notify("Success", "Success", "toaster", notificationType: NotificationType.success);
            var WMRViewModel = new WMRViewModel();
            return View(WMRViewModel);
        }

        public async Task<IActionResult> CreateWMR()
        {
            WMRCreateViewModel wmrCreateViewModel = new();

            return View(wmrCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWMR([FromForm] WMRCreateViewModel wmrCreateViewModel)
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
                    return View(wmrCreateViewModel);
                }
            }
            catch (Exception ex)
            {
                Notify("Exception", ex.Message + " " + ex.InnerException?.Message, "toaster", NotificationType.error);
            }
            return View(wmrCreateViewModel);
        }
    }
}