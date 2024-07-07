using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceHub.WebApp.Controllers;
using ServiceHub.WebApp.Models;

namespace ServiceHub.WebApp.Areas.Masters.Controllers
{
    [Area("Masters")]
    public class UsersClientController : BaseController
    {
        public IActionResult Index()
        {
            UserRegistrationViewModel userRegistrationViewModel = new UserRegistrationViewModel();

            var listFirstNameLastName = TestList("FirstName/LastName ");
            ViewData["FirstNameLastNameList"] = new SelectList(listFirstNameLastName, "DataValueField", "DataTextField");

            var listUsername = TestList("Username ");
            ViewData["UsernameList"] = new SelectList(listUsername, "DataValueField", "DataTextField");

            var listUID = TestList("UID ");
            ViewData["UIDList"] = new SelectList(listUID, "DataValueField", "DataTextField");

            return View(userRegistrationViewModel);
        }

        public IActionResult CreateClientUserIndex()
        {
            var userRegistrationCreateViewModel = new UserRegistrationCreateViewModel();
            return View(userRegistrationCreateViewModel);
        }
    }
}