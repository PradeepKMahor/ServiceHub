using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Controllers;
using ServiceHub.WebApp.Models;

namespace ServiceHub.WebApp.Areas.Masters.Controllers
{
    [Area("Masters")]
    [Authorize]
    public class CustomerProductProfileController : BaseController
    {
        public IActionResult Index()
        {
            CustomerProductProfileViewModel customerProductProfileViewModel = new CustomerProductProfileViewModel();
            return View(customerProductProfileViewModel);
        }

        public IActionResult DetailIndex(string id)
        {
            CustomerProductProfileDetailViewModel customerProductProfileDetailViewModel = new CustomerProductProfileDetailViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                customerProductProfileDetailViewModel.CustomerID = id;
                customerProductProfileDetailViewModel.CustomerName = "Pradeep Mahor";
                customerProductProfileDetailViewModel.CustomerGST = "009865467474";
                customerProductProfileDetailViewModel.Status = "Active";
                customerProductProfileDetailViewModel.Email1 = "P.Mahor@tech.in";
                customerProductProfileDetailViewModel.Email2 = "Sp.GGsd@tech.in";
                customerProductProfileDetailViewModel.ValidFromDate = "29-Jan-2014";
                customerProductProfileDetailViewModel.ValidToDate = "31-Dec-2015";
                customerProductProfileDetailViewModel.Contact1 = "8457124578";
                customerProductProfileDetailViewModel.Contact2 = "5454354354";
                customerProductProfileDetailViewModel.CustomerAddress = "Vashi , new mumbai. Maharashtra ind";
                customerProductProfileDetailViewModel.CountryCode = "12145";
            }
            return View(customerProductProfileDetailViewModel);
        }
    }
}