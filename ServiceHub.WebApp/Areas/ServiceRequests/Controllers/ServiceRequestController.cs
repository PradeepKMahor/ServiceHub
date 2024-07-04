using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Controllers;
using ServiceHub.WebApp.Models;

namespace ServiceHub.WebApp.Areas.ServiceRequests.Controllers
{
    [Area("ServiceRequests")]
    public class ServiceRequestController : BaseController
    {
        public IActionResult Index()
        {
            //Notify("Success", "Success", "toaster", notificationType: NotificationType.success);
            var serviceRequestViewModel = new ServiceRequestViewModel();
            return View(serviceRequestViewModel);
        }

        public async Task<IActionResult> CreateServiceRequest()
        {
            ServiceRequestCreateViewModel serviceRequestCreateViewModel = new();

            return View(serviceRequestCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateServiceRequest([FromForm] ServiceRequestCreateViewModel serviceRequestCreateViewModel)
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
                    return View(serviceRequestCreateViewModel);
                }
            }
            catch (Exception ex)
            {
                Notify("Exception", ex.Message + " " + ex.InnerException?.Message, "toaster", NotificationType.error);
            }
            return View(serviceRequestCreateViewModel);
        }
    }
}