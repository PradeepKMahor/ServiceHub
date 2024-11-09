using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.WebApp.Controllers;
using ServiceHub.WebApp.Models;
using StackExchange.Redis;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

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
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.RoleName = model.RoleName.ToString();

                    var IsRoleExist = await roleManager.FindByIdAsync(model.RoleName);

                    if (await roleManager.RoleExistsAsync(model.RoleName))
                    {
                        Notify("Error", "Role Already exist", "toaster", notificationType: NotificationType.error);
                        return View(model);
                    }
                    IdentityResult result = await roleManager.CreateAsync(new IdentityRole(model.RoleName));
                    if (result.Succeeded)
                    {
                        Notify("Success", "Data saved successfully", "toaster", notificationType: NotificationType.success);
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        Notify("Error", result.Errors.ToString(), "toaster", notificationType: NotificationType.error);
                        return View(model);
                    }
                }
                else
                {
                    Notify("Error", "Role name not found", "toaster", notificationType: NotificationType.error);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                Notify("Error", ex.Message, "toaster", notificationType: NotificationType.error);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    Notify("Success", "Record Deleted successfully.", "toaster", notificationType: NotificationType.success);
                    return Json(new { success = true, message = "Record Deleted successfully." });
                }
                else
                    return Json(new { success = false, message = "Error while deleting.." });
                //Errors(result);
            }
            return Json(new { success = false, message = "Error while deleting.." });
        }
    }
}