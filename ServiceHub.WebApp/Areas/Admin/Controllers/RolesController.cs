using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Controllers;
using ServiceHub.WebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : BaseController
    {
        private RoleManager<IdentityRole> roleManager;

        public RolesController(RoleManager<IdentityRole> roleMgr)
        {
            roleManager = roleMgr;
        }

        public ViewResult Index() => View(roleManager.Roles);

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        public IActionResult CreateRole() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole([Required] string name)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                    if (result.Succeeded)
                    {
                        Notify("Success", "Data saved successfully", "toaster", notificationType: NotificationType.success);
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        Notify("Error", result.Errors.ToString(), "toaster", notificationType: NotificationType.error);
                        return View(name);
                    }
                }
                else
                {
                    Notify("Error", "Role name not found", "toaster", notificationType: NotificationType.error);
                    return View(name);
                }
            }
            catch (Exception ex)
            {
                Notify("Error", ex.Message, "toaster", notificationType: NotificationType.error);
            }

            return View(name);
        }
    }
}