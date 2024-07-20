using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Controllers;
using ServiceHub.WebApp.Models;

namespace ServiceHub.WebApp.Areas.ServiceRequests.Controllers
{
    [Area("ServiceRequests")]
    public class PMRController : BaseController
    {
        public IActionResult Index()
        {
            //Notify("Success", "Success", "toaster", notificationType: NotificationType.success);
            var pmrViewModel = new PMRViewModel();
            return View(pmrViewModel);
        }

        public async Task<IActionResult> CreatePMR()
        {
            PMRCreateViewModel PMRCreateViewModel = new();

            return View(PMRCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePMR([FromForm] PMRCreateViewModel pmrCreateViewModel)
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
                    return View(pmrCreateViewModel);
                }
            }
            catch (Exception ex)
            {
                Notify("Exception", ex.Message + " " + ex.InnerException?.Message, "toaster", NotificationType.error);
            }
            return View(pmrCreateViewModel);
        }
    }
}