using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServiceHub.DataAccess.Models;
using ServiceHub.DataAccess.Repositories.Core;
using ServiceHub.Domain.Models.Data;
using ServiceHub.WebApp.Controllers;
using ServiceHub.WebApp.Models;
using static ServiceHub.WebApp.Models.DTModel;

namespace ServiceHub.WebApp.Areas.Masters.Controllers
{
    [Area("Masters")]
    public class UsersClientController : BaseController
    {
        private readonly IUserClintRepository _userClintRepository;
        private readonly IWebHostEnvironment _WebHostEnvironment;

        public UsersClientController(IUserClintRepository userClintRepository, IWebHostEnvironment webHostEnvironment)
        {
            _userClintRepository = userClintRepository;
            _WebHostEnvironment = webHostEnvironment;
        }

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
        public async Task<JsonResult> GetListsUsersClint(string paramJson)
        {
            DTResult<TblUserClint> result = new();
            var param = JsonConvert.DeserializeObject<DTParameters>(paramJson);
            var data = (await _userClintRepository.GetAllAsync()).AsQueryable();

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

        public IActionResult CreateClientUserIndex()
        {
            List<DataField> parentOrgList = new()
            {
                new DataField { DataTextField = "ParentOrg - 1", DataValueField ="ParentOrg - 1"},
                new DataField { DataTextField = "ParentOrg - 2", DataValueField ="ParentOrg - 2"},
                new DataField { DataTextField = "ParentOrg - 3", DataValueField ="ParentOrg - 3"},
                new DataField { DataTextField = "ParentOrg - 4", DataValueField ="ParentOrg - 4"}
            };

            ViewData["ParentOrg"] = new SelectList(parentOrgList.ToList(), "DataValueField", "DataTextField");

            List<DataField> userTypeList = new()
            {
                new DataField { DataTextField = "Customer", DataValueField ="Customer"}
            };
            ViewData["UserType"] = new SelectList(userTypeList, "DataValueField", "DataTextField");

            var userRegistrationCreateViewModel = new UserRegistrationCreateViewModel();
            return View(userRegistrationCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateClientUserIndex(UserRegistrationCreateViewModel userRegistrationCreateViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string strFilePath = @"Imgs\ClintUser";
                    string strFolderPath = @"\Imgs\ClintUser\";
                    string webRootPath = _WebHostEnvironment.WebRootPath;
                    var files = HttpContext.Request.Form.Files;
                    // New Service
                    string fileName = Guid.NewGuid().ToString();
                    var upload = Path.Combine(webRootPath, strFilePath);
                    var extention = Path.GetExtension(files[0].FileName);
                    TblUserClint tblUserClint = new TblUserClint();
                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extention), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);

                        userRegistrationCreateViewModel.UploadProfilePic = strFolderPath + fileName + extention;
                    }
                    if (userRegistrationCreateViewModel.ActiveStatus)
                    {
                        tblUserClint.ActiveStatus = "Active";
                    }
                    else
                    {
                        tblUserClint.ActiveStatus = "DeActive";
                    }
                    tblUserClint.Username = userRegistrationCreateViewModel.Username;
                    tblUserClint.FirstName = userRegistrationCreateViewModel.FirstName;
                    tblUserClint.MiddleName = userRegistrationCreateViewModel.MiddleName;
                    tblUserClint.LastName = userRegistrationCreateViewModel.LastName;
                    tblUserClint.ContactNo = userRegistrationCreateViewModel.ContactNo;
                    tblUserClint.EmailId = userRegistrationCreateViewModel.EmailId;
                    //tblUserClint.AdminName = userRegistrationCreateViewModel.AdminName;
                    tblUserClint.UploadProfilePic = userRegistrationCreateViewModel.UploadProfilePic;
                    tblUserClint.ValidFromDate = userRegistrationCreateViewModel.ValidFromDate;
                    tblUserClint.ValidToDate = userRegistrationCreateViewModel.ValidToDate;
                    //tblUserClint.UserType = userRegistrationCreateViewModel.UserType;
                    tblUserClint.UserType = "Clint";
                    tblUserClint.UserId = tblUserClint.Username + "_" + tblUserClint.LastName;
                    tblUserClint.Password = userRegistrationCreateViewModel.Username + "_" + userRegistrationCreateViewModel.LastName;
                    tblUserClint.SupervisorName = "AvinashK";

                    _userClintRepository.InsertAsync(tblUserClint);
                    Notify("Success", "Data saved successfully", "toaster", notificationType: Models.NotificationType.success);

                    return RedirectToAction(nameof(CreateClientUserIndex));
                }
                else
                {
                    Notify("Error", "Something Missing Or Data Not Found", "toaster", notificationType: Models.NotificationType.error);
                    return RedirectToAction(nameof(CreateClientUserIndex));
                }
            }
            catch (Exception ex)
            {
                Notify("Error", ex.Message, "toaster", notificationType: Models.NotificationType.error);
            }

            return View(userRegistrationCreateViewModel);
        }

        public async Task<IActionResult> UpdateClientUserIndexAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var result = await _userClintRepository.GetAsync(m => m.Id == id);

            if (result == null)
            {
                return NotFound();
            }
            var userRegistrationCreateViewModel = new UserRegistrationCreateViewModel();
            var model = new UserRegistrationCreateViewModel
            {
                ContactNo = result.ContactNo,
                EmailId = result.EmailId,
                FirstName = result.FirstName,
                MiddleName = result.MiddleName,
                LastName = result.LastName,

                Id = result.Id,
                // ParentOrg = result.ParentOrg,

                UploadProfilePic = result.UploadProfilePic,
                Username = result.Username,

                ValidFromDate = result.ValidFromDate,
                ValidToDate = result.ValidToDate
            };
            if (result.ActiveStatus == "Active")
            {
                model.ActiveStatus = true;
            }
            else
            {
                model.ActiveStatus = false;
            }
            List<DataField> parentOrgList = new()
            {
                new DataField { DataTextField = "ParentOrg - 1", DataValueField ="ParentOrg - 1"},
                new DataField { DataTextField = "ParentOrg - 2", DataValueField ="ParentOrg - 2"},
                new DataField { DataTextField = "ParentOrg - 3", DataValueField ="ParentOrg - 3"},
                new DataField { DataTextField = "ParentOrg - 4", DataValueField ="ParentOrg - 4"}
            };

            ViewData["ParentOrg"] = new SelectList(parentOrgList.ToList(), "DataValueField", "DataTextField");

            List<DataField> userTypeList = new()
            {
                new DataField { DataTextField = "Customer", DataValueField ="Customer"}
            };
            ViewData["UserType"] = new SelectList(userTypeList, "DataValueField", "DataTextField");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateClientUserIndex(UserRegistrationCreateViewModel userRegistrationCreateViewModel)
        {
            try
            {
                if (userRegistrationCreateViewModel.Id == 0)
                {
                    Notify("Error", "Something Missing Or Data Not Found", "toaster", notificationType: Models.NotificationType.error);
                    return RedirectToAction(nameof(CreateClientUserIndex));
                }
                TblUserClint tblUserClint = new TblUserClint();
                if (ModelState.IsValid)
                {
                    string strFilePath = @"Imgs\ClintUser";
                    string strFolderPath = @"\Imgs\ClintUser\";
                    string webRootPath = _WebHostEnvironment.WebRootPath;
                    var files = HttpContext.Request.Form.Files;
                    var fromDb = _userClintRepository.GetAsync(m => m.Id == userRegistrationCreateViewModel.Id);

                    if (files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var upload = Path.Combine(webRootPath, strFilePath);
                        var extention_new = Path.GetExtension(files[0].FileName);

                        var imagePath = Path.Combine(webRootPath, fromDb.Result.UploadProfilePic.TrimStart('\\'));

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extention_new), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        userRegistrationCreateViewModel.UploadProfilePic = strFolderPath + fileName + extention_new;
                    }
                    else
                    {
                        userRegistrationCreateViewModel.UploadProfilePic = fromDb.Result.UploadProfilePic;
                    }

                    if (userRegistrationCreateViewModel.ActiveStatus)
                    {
                        tblUserClint.ActiveStatus = "Active";
                    }
                    else
                    {
                        tblUserClint.ActiveStatus = "DeActive";
                    }
                    tblUserClint.ActiveStatus = "Active";
                    tblUserClint.Id = (int)userRegistrationCreateViewModel.Id;
                    tblUserClint.Username = userRegistrationCreateViewModel.Username;
                    tblUserClint.FirstName = userRegistrationCreateViewModel.FirstName;
                    tblUserClint.MiddleName = userRegistrationCreateViewModel.MiddleName;
                    tblUserClint.LastName = userRegistrationCreateViewModel.LastName;
                    tblUserClint.ContactNo = userRegistrationCreateViewModel.ContactNo;
                    tblUserClint.EmailId = userRegistrationCreateViewModel.EmailId;
                    //tblUserClint.AdminName = userRegistrationCreateViewModel.AdminName;
                    tblUserClint.UploadProfilePic = userRegistrationCreateViewModel.UploadProfilePic;
                    tblUserClint.ValidFromDate = userRegistrationCreateViewModel.ValidFromDate;
                    tblUserClint.ValidToDate = userRegistrationCreateViewModel.ValidToDate;
                    //tblUserClint.UserType = userRegistrationCreateViewModel.UserType;
                    tblUserClint.UserType = "Clint";
                    tblUserClint.UserId = tblUserClint.Username + "_" + tblUserClint.LastName;
                    tblUserClint.Password = userRegistrationCreateViewModel.Username + "_" + userRegistrationCreateViewModel.LastName;
                    tblUserClint.SupervisorName = "AvinashK";

                    _userClintRepository.UpdateAsync(tblUserClint);
                    Notify("Success", "Data updated successfully", "toaster", notificationType: Models.NotificationType.success);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Notify("Error", "Something Missing Or Data Not Found", "toaster", notificationType: Models.NotificationType.error);
                    return RedirectToAction(nameof(CreateClientUserIndex));
                }
            }
            catch (Exception ex)
            {
                Notify("Error", ex.Message, "toaster", notificationType: Models.NotificationType.error);
            }

            return View(userRegistrationCreateViewModel);
        }
    }
}