using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Controllers;
using ServiceHub.WebApp.Models;

namespace ServiceHub.WebApp.Areas.Masters.Controllers
{
    [Area("Masters")]
    public class CustomerProductProfileController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DetailIndex(string id)
        {
            CustomerProductProfileDetailViewModel customerProductProfileDetailViewModel = new CustomerProductProfileDetailViewModel();
            customerProductProfileDetailViewModel.CustomerID = id;
            customerProductProfileDetailViewModel.CustomerName = "Pradeep Mahor";
            customerProductProfileDetailViewModel.CustomerGST = "009865467474";
            customerProductProfileDetailViewModel.Status = "Active";
            customerProductProfileDetailViewModel.Email = "P.Mahor@tech.in";
            customerProductProfileDetailViewModel.ValidFromDate = "29-Jan-2014";
            customerProductProfileDetailViewModel.ValidToDate = "31-Dec-2015";
            customerProductProfileDetailViewModel.Contact = "8457124578";
            customerProductProfileDetailViewModel.CustomerAddress = "Vashi , new mumbai. Maharashtra ind";
            customerProductProfileDetailViewModel.CountryCode = "12145";
            return View(customerProductProfileDetailViewModel);
        }
    }
}