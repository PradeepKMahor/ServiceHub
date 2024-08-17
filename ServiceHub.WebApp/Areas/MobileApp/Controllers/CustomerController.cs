using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Controllers;
using ServiceHub.WebApp.Models;

namespace ServiceHub.WebApp.Areas.MobileApp.Controllers
{
    [Area("MobileApp")]
    public class CustomerController : BaseController
    {
        public IActionResult Feedback()
        {
            CMRFeedbackViewModel model = new CMRFeedbackViewModel();
            return View(model);
        }

        public IActionResult ContactIndex()
        {
            ContactViewModel model = new ContactViewModel();
            return View(model);
        }

        public IActionResult WorkReview()
        {
            WorkReviewViewModel model = new WorkReviewViewModel();
            return View(model);
        }

        public IActionResult Notifications()
        {
            NotificationsViewModel model = new NotificationsViewModel();
            return View(model);
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

        #region From HomeIndex

        public IActionResult Index()
        {
            var cmrViewModel = new CMRViewModel();
            return View(cmrViewModel);
        }

        public IActionResult DashboardIndex()
        {
            var cmrViewModel = new CMRViewModel();
            return View(cmrViewModel);
        }

        public IActionResult PMRDashboardIndex()
        {
            var pmrViewModel = new PMRViewModel();
            return View(pmrViewModel);
        }

        public IActionResult WMRDashboardIndex()
        {
            var cmrViewModel = new CMRViewModel();
            return View(cmrViewModel);
        }

        public IActionResult CMRIndex()
        {
            var cmrViewModel = new CMRViewModel();
            return View(cmrViewModel);
        }

        public IActionResult PMRIndex()
        {
            var cmrViewModel = new CMRViewModel();
            return View(cmrViewModel);
        }

        public IActionResult WMRIndex()
        {
            var cmrViewModel = new CMRViewModel();
            return View(cmrViewModel);
        }

        //public IActionResult DashboardIndex()
        //{
        //    return View();
        //}

        public IActionResult AboutUsIndex()
        {
            return View();
        }

        public IActionResult ContactUsIndex()
        {
            return View();
        }

        #endregion From HomeIndex
    }
}