using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServiceHub.DataAccess.Models;
using ServiceHub.Domain;
using ServiceHub.Domain.Models.Data;
using ServiceHub.WebApp.Controllers;
using ServiceHub.WebApp.Models;
using static ServiceHub.WebApp.Models.DTModel;

namespace ServiceHub.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Administrator)]
    public class UsersController : BaseController
    {
        private UserManager<AppUser> userManager;
        private IPasswordHasher<AppUser> passwordHasher;
        private RoleManager<IdentityRole> roleManager;
        private readonly IWebHostEnvironment _WebHostEnvironment;

        public UsersController(UserManager<AppUser> usrMgr,
            IPasswordHasher<AppUser> passwordHash,
            RoleManager<IdentityRole> roleManager,
            IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = usrMgr;
            this.passwordHasher = passwordHash;
            this.roleManager = roleManager;
            _WebHostEnvironment = webHostEnvironment;
        }

        /*private IPasswordValidator<AppUser> passwordValidator;
        private IUserValidator<AppUser> userValidator;

        public AdminController(UserManager<AppUser> usrMgr, IPasswordHasher<AppUser> passwordHash, IPasswordValidator<AppUser> passwordVal, IUserValidator<AppUser> userValid)
        {
            userManager = usrMgr;
            passwordHasher = passwordHash;
            passwordValidator = passwordVal;
            userValidator = userValid;
        }*/

        public IActionResult Index()
        {
            UserRegistrationViewModel userRegistrationViewModel = new UserRegistrationViewModel();

            var listFirstNameLastName = TestList("FirstName/LastName ");
            ViewData["FirstNameLastNameList"] = new SelectList(listFirstNameLastName, "DataValueField", "DataTextField");

            var listUsername = TestList("Username");
            ViewData["UsernameList"] = new SelectList(listUsername, "DataValueField", "DataTextField");

            var listParentOrg = TestList("Parent Org. ");
            ViewData["ParentOrgList"] = new SelectList(listParentOrg, "DataValueField", "DataTextField");

            return View(userRegistrationViewModel);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> GetListsUsers(string paramJson)
        {
            DTResult<AppUser> result = new();
            var param = JsonConvert.DeserializeObject<DTParameters>(paramJson);
            var data = (userManager.Users).AsQueryable();

            int totalRow = 0;

            try
            {
                //var data = (await _dataRepository.GetAllAsync()).AsQueryable();

                //if (param.Start is 0)
                //{
                //    param.Start = 1;
                //}
                // Filtering
                if (!string.IsNullOrEmpty(param.GenericSearch))
                {
                    data = data
                            .Where(t => t.FirstName.Contains(param.GenericSearch, StringComparison.InvariantCultureIgnoreCase)
                            | t.FirstName.Contains(param.GenericSearch, StringComparison.InvariantCultureIgnoreCase));
                }

                // Sorting
                //if (param.Order != null)
                //    foreach (var sortcolumn in param.Order)
                //    {
                //        data = data.Order(param.Columns[sortcolumn.Column].Data + " " + sortcolumn.Dir.ToString());
                //    }

                //if (data.Any())
                //{
                //    totalRow = (int)data.FirstOrDefault().TotalRow.Value;
                //}

                //result.draw = param.Draw;
                //result.recordsTotal = totalRow;
                //result.recordsFiltered = totalRow;
                //result.data = data.ToList();

                result.draw = param.Draw;
                result.recordsTotal = data.Count();
                result.recordsFiltered = data.Count();
                result.data = data.Skip(param.Start).Take(param.Length).AsNoTracking().ToList();

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        public IActionResult CreateUser()
        {
            var model = new UserViewModel();
            List<DataField> parentOrgList = new()
            {
                new DataField { DataTextField = "ParentOrg - 1", DataValueField ="ParentOrg - 1"},
                new DataField { DataTextField = "ParentOrg - 2", DataValueField ="ParentOrg - 2"},
                new DataField { DataTextField = "ParentOrg - 3", DataValueField ="ParentOrg - 3"},
                new DataField { DataTextField = "ParentOrg - 4", DataValueField ="ParentOrg - 4"}
            };

            ViewData["ParentOrg"] = new SelectList(parentOrgList.ToList(), "DataValueField", "DataTextField");

            List<DataField> userTypeList = new();
            foreach (var item in roleManager.Roles)
            {
                var ddlItem = new DataField { DataTextField = item.Name, DataValueField = item.Name };

                userTypeList.Add(ddlItem);
            }

            ViewData["UserType"] = new SelectList(userTypeList, "DataValueField", "DataTextField");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(UserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AppUser appUser = new AppUser();

                    appUser.UserName = model.EmailId;
                    appUser.Email = model.EmailId;
                    //TwoFactorEnabled = true

                    string strFilePath = @"Imgs\AppUsers";
                    string strFolderPath = @"\Imgs\AppUsers\";
                    string webRootPath = _WebHostEnvironment.WebRootPath;
                    var files = HttpContext.Request.Form.Files;

                    string fileName = Guid.NewGuid().ToString();

                    if (files.Count > 0)
                    {
                        var upload = Path.Combine(webRootPath, strFilePath);
                        var extention = Path.GetExtension(files[0].FileName);

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extention), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);

                            model.UploadProfilePic = strFolderPath + fileName + extention;
                        }
                    }

                    appUser.ActiveStatus = model.ActiveStatus;

                    if (appUser.ActiveStatus)
                    {
                        appUser.LockoutEnabled = false;
                        appUser.LockoutEnd = DateTime.Now;
                    }
                    else
                    {
                        appUser.LockoutEnabled = true;
                        appUser.LockoutEnd = DateTime.Now.AddYears(1);
                    }

                    appUser.UserId = model.Username;
                    appUser.FirstName = model.FirstName;
                    appUser.MiddleName = model.MiddleName;
                    appUser.LastName = model.LastName;
                    appUser.ContactNo = model.ContactNo;
                    appUser.EmailId = model.EmailId;
                    appUser.UploadProfilePic = model.UploadProfilePic;
                    appUser.ValidFromDate = model.ValidFromDate;
                    appUser.ValidToDate = model.ValidToDate;
                    appUser.ParentOrg = model.ParentOrg;
                    appUser.UserType = model.UserType;
                    appUser.EmailConfirmed = true;
                    appUser.Password = model.Password;
                    appUser.SupervisorName = "AvinashK";

                    IdentityResult result = await userManager.CreateAsync(appUser, model.Password);

                    /*if (result.Succeeded)
                    {
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(appUser);
                        var confirmationLink = Url.Action("ConfirmEmail", "Email", new { token, email = user.Email }, Request.Scheme);
                        EmailHelper emailHelper = new EmailHelper();
                        bool emailResponse = emailHelper.SendEmail(user.Email, confirmationLink);

                        if (emailResponse)
                            return RedirectToAction("Index");
                        else
                        {
                            // log email failed
                        }
                    }*/

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(appUser, model.UserType);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                            ModelState.AddModelError("", error.Description);
                    }
                }
                else
                {
                    Notify("Error", "Something Missing Or Data Not Found", "toaster", notificationType: Models.NotificationType.error);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                Notify("Error", ex.Message, "toaster", notificationType: Models.NotificationType.error);
            }
            List<DataField> parentOrgList = new()
            {
                new DataField { DataTextField = "ParentOrg - 1", DataValueField ="ParentOrg - 1"},
                new DataField { DataTextField = "ParentOrg - 2", DataValueField ="ParentOrg - 2"},
                new DataField { DataTextField = "ParentOrg - 3", DataValueField ="ParentOrg - 3"},
                new DataField { DataTextField = "ParentOrg - 4", DataValueField ="ParentOrg - 4"}
            };

            ViewData["ParentOrg"] = new SelectList(parentOrgList.ToList(), "DataValueField", "DataTextField", model.ParentOrg);

            List<DataField> userTypeList = new();
            foreach (var item in roleManager.Roles)
            {
                var ddlItem = new DataField { DataTextField = item.Name, DataValueField = item.Name };

                userTypeList.Add(ddlItem);
            }

            ViewData["UserType"] = new SelectList(userTypeList, "DataValueField", "DataTextField", model.UserType);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserDetails(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await userManager.FindByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            var model = new UserViewModel
            {
                AdminName = result.AdminName,
                ContactNo = result.ContactNo,
                EmailId = result.EmailId,
                FirstName = result.FirstName,
                MiddleName = result.MiddleName,
                LastName = result.LastName,
                Password = result.Password,
                Id = result.Id,
                ParentOrg = result.ParentOrg,
                SupervisorName = result.SupervisorName,
                UploadProfilePic = result.UploadProfilePic,
                Username = result.UserName,
                UserType = result.UserType,
                ValidFromDate = result.ValidFromDate,
                ValidToDate = result.ValidToDate
            };

            model.ActiveStatus = true;

            List<DataField> parentOrgList = new()
            {
                new DataField { DataTextField = "ParentOrg - 1", DataValueField ="ParentOrg - 1"},
                new DataField { DataTextField = "ParentOrg - 2", DataValueField ="ParentOrg - 2"},
                new DataField { DataTextField = "ParentOrg - 3", DataValueField ="ParentOrg - 3"},
                new DataField { DataTextField = "ParentOrg - 4", DataValueField ="ParentOrg - 4"}
            };

            ViewData["ParentOrg"] = new SelectList(parentOrgList, "DataValueField", "DataTextField", result.ParentOrg);

            List<DataField> userTypeList = new();
            foreach (var item in roleManager.Roles)
            {
                var ddlItem = new DataField { DataTextField = item.Name, DataValueField = item.Name };

                userTypeList.Add(ddlItem);
            }

            ViewData["UserType"] = new SelectList(userTypeList, "DataValueField", "DataTextField", model.UserType);

            return View(model);
        }

        public async Task<IActionResult> Update(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, string email, string password)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            //user.TwoFactorEnabled = true;
            if (user != null)
            {
                if (!string.IsNullOrEmpty(email))
                    user.Email = email;
                else
                    ModelState.AddModelError("", "Email cannot be empty");

                if (!string.IsNullOrEmpty(password))
                    user.PasswordHash = passwordHasher.HashPassword(user, password);
                else
                    ModelState.AddModelError("", "Password cannot be empty");

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        Errors(result);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(user);
        }

        /*[HttpPost]
        public async Task<IActionResult> Update(string id, string email, string password)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult validEmail = null;
                if (!string.IsNullOrEmpty(email))
                {
                    validEmail = await userValidator.ValidateAsync(userManager, user);
                    if (validEmail.Succeeded)
                        user.Email = email;
                    else
                        Errors(validEmail);
                }
                else
                    ModelState.AddModelError("", "Email cannot be empty");

                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(password))
                {
                    validPass = await passwordValidator.ValidateAsync(userManager, user, password);
                    if (validPass.Succeeded)
                        user.PasswordHash = passwordHasher.HashPassword(user, password);
                    else
                        Errors(validPass);
                }
                else
                    ModelState.AddModelError("", "Password cannot be empty");

                if (validEmail != null && validPass != null && validEmail.Succeeded && validPass.Succeeded)
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        Errors(result);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");

            return View(user);
        }*/

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View("Index", userManager.Users);
        }

        public async Task<IActionResult> LockAsync(string id)
        {
            try
            {
                if (id == null)
                {
                    Notify("Error", "NotFound", "toaster", notificationType: Models.NotificationType.error);
                }
                else
                {
                    AppUser user = await userManager.FindByIdAsync(id);
                    userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddYears(1));
                }
            }
            catch (Exception ex)
            {
                Notify("Error", ex.Message, "toaster", notificationType: Models.NotificationType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UnLockAsync(string id)
        {
            try
            {
                if (id == null)
                {
                    Notify("Error", "NotFound", "toaster", notificationType: Models.NotificationType.error);
                }
                else
                {
                    AppUser user = await userManager.FindByIdAsync(id);
                    userManager.SetLockoutEndDateAsync(user, DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                Notify("Error", ex.Message, "toaster", notificationType: Models.NotificationType.error);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}