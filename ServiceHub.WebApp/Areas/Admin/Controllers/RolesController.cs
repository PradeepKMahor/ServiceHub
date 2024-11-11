using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.Domain;
using ServiceHub.WebApp.Controllers;
using ServiceHub.WebApp.Models;

namespace ServiceHub.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Administrator)]
    public class RolesController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;

        public RolesController(RoleManager<IdentityRole> roleMgr, UserManager<AppUser> userManager)
        {
            roleManager = roleMgr;
            this.userManager = userManager;
        }

        public ViewResult Index()
        {
            return View(roleManager.Roles);
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.RoleName = model.RoleName.ToString();

                    IdentityRole? IsRoleExist = await roleManager.FindByIdAsync(model.RoleName);

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

        public async Task<IActionResult> UpdateRole(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<AppUser> members = new();
            List<AppUser> nonMembers = new();
            foreach (AppUser user in userManager.Users)
            {
                List<AppUser> list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleEdit
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(RoleModification model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    AppUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    AppUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
            }

            return ModelState.IsValid ? (IActionResult)RedirectToAction(nameof(Index)) : await UpdateRole(model.RoleId);
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

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}