using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServiceHub.DataAccess.Interface.Core;
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

        public IActionResult CreateClientUser()
        {
            var viewModel = new UserRegistrationCreateViewModel();

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
                new DataField { DataTextField = "ClientUser", DataValueField ="ClientUser"},
                new DataField { DataTextField = "Administrator", DataValueField ="Administrator"},
                new DataField { DataTextField = "Technion", DataValueField ="Technion"},
                new DataField { DataTextField = "Supervisor", DataValueField ="Supervisor"}
            };
            ViewData["UserType"] = new SelectList(userTypeList, "DataValueField", "DataTextField");

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateClientUser(UserRegistrationCreateViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string strFilePath = @"Imgs\UsersClient";
                    string strFolderPath = @"\Imgs\UsersClient\";
                    string webRootPath = _WebHostEnvironment.WebRootPath;
                    var files = HttpContext.Request.Form.Files;

                    string fileName = Guid.NewGuid().ToString();
                    TblUserClint tblUserClint = new TblUserClint();
                    if (files != null)
                    {
                        var upload = Path.Combine(webRootPath, strFilePath);
                        var extention = Path.GetExtension(files[0].FileName);

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extention), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);

                            viewModel.UploadProfilePic = strFolderPath + fileName + extention;
                        }
                    }

                    if (viewModel.ActiveStatus)
                    {
                        tblUserClint.ActiveStatus = "Active";
                    }
                    else
                    {
                        tblUserClint.ActiveStatus = "DeActive";
                    }
                    tblUserClint.Username = viewModel.Username;
                    tblUserClint.FirstName = viewModel.FirstName;
                    tblUserClint.MiddleName = viewModel.MiddleName;
                    tblUserClint.LastName = viewModel.LastName;
                    tblUserClint.ContactNo = viewModel.ContactNo;
                    tblUserClint.EmailId = viewModel.EmailId;
                    //tblUserClint.AdminName = viewModel.AdminName;
                    tblUserClint.UploadProfilePic = viewModel.UploadProfilePic;
                    tblUserClint.ValidFromDate = viewModel.ValidFromDate;
                    tblUserClint.ValidToDate = viewModel.ValidToDate;
                    tblUserClint.ParentOrg = viewModel.ParentOrg;
                    tblUserClint.UserType = viewModel.UserType;
                    tblUserClint.UserId = tblUserClint.Username + "_" + tblUserClint.LastName;
                    tblUserClint.Password = viewModel.Username + "_" + viewModel.LastName;
                    tblUserClint.SupervisorName = "AvinashK";

                    _userClintRepository.InsertAsync(tblUserClint);
                    Notify("Success", "Data saved successfully", "toaster", notificationType: Models.NotificationType.success);

                    return RedirectToAction(nameof(CreateClientUser));
                }
                else
                {
                    Notify("Error", "Something Missing Or Data Not Found", "toaster", notificationType: Models.NotificationType.error);
                    return RedirectToAction(nameof(CreateClientUser));
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

            ViewData["ParentOrg"] = new SelectList(parentOrgList.ToList(), "DataValueField", "DataTextField", viewModel.ParentOrg);

            List<DataField> userTypeList = new()
            {
                new DataField { DataTextField = "ClientUser", DataValueField ="ClientUser"},
                new DataField { DataTextField = "Administrator", DataValueField ="Administrator"},
                new DataField { DataTextField = "Technion", DataValueField ="Technion"},
                new DataField { DataTextField = "Supervisor", DataValueField ="Supervisor"}
            };

            ViewData["UserType"] = new SelectList(userTypeList, "DataValueField", "DataTextField", viewModel.UserType);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsClientUser(int? id)
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

            var model = new UserRegistrationCreateViewModel
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
                Username = result.Username,
                UserType = result.UserType,
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

            ViewData["ParentOrg"] = new SelectList(parentOrgList.ToList(), "DataValueField", "DataTextField", model.ParentOrg);

            List<DataField> userTypeList = new()
            {
                new DataField { DataTextField = "ClientUser", DataValueField ="ClientUser"},
                new DataField { DataTextField = "Administrator", DataValueField ="Administrator"},
                new DataField { DataTextField = "Technion", DataValueField ="Technion"},
                new DataField { DataTextField = "Supervisor", DataValueField ="Supervisor"}
            };

            ViewData["UserType"] = new SelectList(userTypeList, "DataValueField", "DataTextField", model.UserType);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateClientUserAjax(int? id)
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

            var model = new UserRegistrationCreateViewModel
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
                Username = result.Username,
                UserType = result.UserType,
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

            ViewData["ParentOrg"] = new SelectList(parentOrgList.ToList(), "DataValueField", "DataTextField", model.ParentOrg);

            List<DataField> userTypeList = new()
            {
                new DataField { DataTextField = "ClientUser", DataValueField ="ClientUser"},
                new DataField { DataTextField = "Administrator", DataValueField ="Administrator"},
                new DataField { DataTextField = "Technion", DataValueField ="Technion"},
                new DataField { DataTextField = "Supervisor", DataValueField ="Supervisor"}
            };

            ViewData["UserType"] = new SelectList(userTypeList, "DataValueField", "DataTextField", model.UserType);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateClientUser(UserRegistrationCreateViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //TblUserCustomer tblUserCustomer = new TblUserCustomer();
                    string strFilePath = @"Imgs\UsersClient";
                    string strFolderPath = @"\Imgs\UsersClient\";
                    string webRootPath = _WebHostEnvironment.WebRootPath;
                    var files = HttpContext.Request.Form.Files;
                    var fromDb = _userClintRepository.GetAsync(m => m.Id == viewModel.Id);

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
                        viewModel.UploadProfilePic = strFolderPath + fileName + extention_new;
                    }
                    else
                    {
                        viewModel.UploadProfilePic = fromDb.Result.UploadProfilePic;
                    }

                    if (viewModel.ActiveStatus)
                    {
                        fromDb.Result.ActiveStatus = "Active";
                    }
                    else
                    {
                        fromDb.Result.ActiveStatus = "DeActive";
                    }

                    if (viewModel.ActiveStatus)
                    {
                        fromDb.Result.ActiveStatus = "Active";
                    }
                    else
                    {
                        fromDb.Result.ActiveStatus = "DeActive";
                    }

                    fromDb.Result.Username = viewModel.Username;
                    fromDb.Result.FirstName = viewModel.FirstName;
                    fromDb.Result.MiddleName = viewModel.MiddleName;
                    fromDb.Result.LastName = viewModel.LastName;
                    fromDb.Result.ContactNo = viewModel.ContactNo;
                    fromDb.Result.EmailId = viewModel.EmailId;
                    fromDb.Result.AdminName = viewModel.AdminName;
                    fromDb.Result.ParentOrg = viewModel.ParentOrg;
                    fromDb.Result.UploadProfilePic = viewModel.UploadProfilePic;
                    fromDb.Result.ValidFromDate = viewModel.ValidFromDate;
                    fromDb.Result.ValidToDate = viewModel.ValidToDate;
                    fromDb.Result.UserType = viewModel.UserType;
                    fromDb.Result.Password = viewModel.Password;
                    fromDb.Result.UserId = viewModel.ContactNo;
                    fromDb.Result.SupervisorName = "AvinashK";

                    _userClintRepository.UpdateAsync(fromDb.Result);
                    Notify("Success", "Data updated successfully", "toaster", notificationType: Models.NotificationType.success);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Notify("Error", "Something Missing Or Data Not Found", "toaster", notificationType: Models.NotificationType.error);
                    return RedirectToAction(nameof(CreateClientUser));
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

            ViewData["ParentOrg"] = new SelectList(parentOrgList.ToList(), "DataValueField", "DataTextField", viewModel.ParentOrg);

            List<DataField> userTypeList = new()
            {
                new DataField { DataTextField = "ClientUser", DataValueField ="ClientUser"},
                new DataField { DataTextField = "Administrator", DataValueField ="Administrator"},
                new DataField { DataTextField = "Technion", DataValueField ="Technion"},
                new DataField { DataTextField = "Supervisor", DataValueField ="Supervisor"}
            };

            ViewData["UserType"] = new SelectList(userTypeList, "DataValueField", "DataTextField", viewModel.UserType);

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(short? id)
        {
            var result = await _userClintRepository.GetAsync(m => m.Id == id);

            try
            {
                await _userClintRepository.DeleteAsync(result);

                string strFilePath = @"Imgs\UsersClient";
                string strFolderPath = @"\Imgs\UsersClient\";
                string webRootPath = _WebHostEnvironment.WebRootPath;

                var imagePath = Path.Combine(webRootPath, result.UploadProfilePic.TrimStart('\\'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                Notify("Success", "Data deleted successfully", "toaster", notificationType: Models.NotificationType.success);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Notify("Error", ex.Message, "toaster", notificationType: Models.NotificationType.error);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}