using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Controllers;
using ServiceHub.WebApp.Models;

namespace ServiceHub.WebApp.Areas.Technician.Controllers
{
    [Area("Technician")]
    public class ServiceRequestController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateSR()
        {
            CMRCreateViewModel cmrCreateViewModel = new();

            return View(cmrCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSR([FromForm] CMRCreateViewModel cmrCreateViewModel)
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