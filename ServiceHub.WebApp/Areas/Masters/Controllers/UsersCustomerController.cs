using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServiceHub.DataAccess;
using ServiceHub.DataAccess.Base;
using ServiceHub.DataAccess.Interface.Core;
using ServiceHub.DataAccess.Models;
using ServiceHub.DataAccess.Repositories.Core;
using ServiceHub.Domain.Context;
using ServiceHub.Domain.Models.Data;
using ServiceHub.WebApp.Controllers;
using ServiceHub.WebApp.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using static ServiceHub.WebApp.Models.DTModel;

namespace ServiceHub.WebApp.Areas.Masters.Controllers
{
    [Area("Masters")]
    public class UsersCustomerController : BaseController
    {
        //private readonly DataContext _dataContext;
        private readonly IUsersCustomerRepository _usersCustomerRepository;

        private readonly IWebHostEnvironment _WebHostEnvironment;

        public UsersCustomerController(/*DataContext dataContext, */
            IUsersCustomerRepository usersCustomerRepository, IWebHostEnvironment webHostEnvironment)
        {
            //_dataContext = dataContext;
            _usersCustomerRepository = usersCustomerRepository;
            _WebHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var userCustomerViewModel = new UserCustomerViewModel();
            return View(userCustomerViewModel);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> GetListsUsersCustomer(string paramJson)
        {
            DTResult<TblUserCustomer> result = new();
            var param = JsonConvert.DeserializeObject<DTParameters>(paramJson);
            var data = (await _usersCustomerRepository.GetAllAsync()).AsQueryable();

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

        public IActionResult CreateCustomerUser()
        {
            var userCustomerCreateViewModel = new UserCustomerCreateViewModel();

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

            return View(userCustomerCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCustomerUser(UserCustomerCreateViewModel userCustomerCreateViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string strFilePath = @"Imgs\UsersCustomer";
                    string strFolderPath = @"\Imgs\UsersCustomer\";
                    string webRootPath = _WebHostEnvironment.WebRootPath;
                    var files = HttpContext.Request.Form.Files;

                    string fileName = Guid.NewGuid().ToString();
                    TblUserCustomer tblUserCustomer = new TblUserCustomer();
                    if (files != null)
                    {
                        var upload = Path.Combine(webRootPath, strFilePath);
                        var extention = Path.GetExtension(files[0].FileName);

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extention), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);

                            userCustomerCreateViewModel.UploadProfilePic = strFolderPath + fileName + extention;
                        }
                    }

                    if (userCustomerCreateViewModel.ActiveStatus)
                    {
                        tblUserCustomer.ActiveStatus = "Active";
                    }
                    else
                    {
                        tblUserCustomer.ActiveStatus = "DeActive";
                    }
                    tblUserCustomer.Username = userCustomerCreateViewModel.Username;
                    tblUserCustomer.FirstName = userCustomerCreateViewModel.FirstName;
                    tblUserCustomer.MiddleName = userCustomerCreateViewModel.MiddleName;
                    tblUserCustomer.LastName = userCustomerCreateViewModel.LastName;
                    tblUserCustomer.ContactNo = userCustomerCreateViewModel.ContactNo;
                    tblUserCustomer.EmailId = userCustomerCreateViewModel.EmailId;
                    //tblUserCustomer.AdminName = userCustomerCreateViewModel.AdminName;
                    tblUserCustomer.UploadProfilePic = userCustomerCreateViewModel.UploadProfilePic;
                    tblUserCustomer.ValidFromDate = userCustomerCreateViewModel.ValidFromDate;
                    tblUserCustomer.ValidToDate = userCustomerCreateViewModel.ValidToDate;
                    tblUserCustomer.ParentOrg = userCustomerCreateViewModel.ParentOrg;
                    tblUserCustomer.UserType = userCustomerCreateViewModel.UserType;
                    tblUserCustomer.UserId = tblUserCustomer.Username + "_" + tblUserCustomer.LastName;
                    tblUserCustomer.Password = userCustomerCreateViewModel.Username + "_" + userCustomerCreateViewModel.LastName;
                    tblUserCustomer.SupervisorName = "AvinashK";

                    _usersCustomerRepository.InsertAsync(tblUserCustomer);
                    Notify("Success", "Data saved successfully", "toaster", notificationType: Models.NotificationType.success);

                    return RedirectToAction(nameof(CreateCustomerUser));
                }
                else
                {
                    Notify("Error", "Something Missing Or Data Not Found", "toaster", notificationType: Models.NotificationType.error);
                    return RedirectToAction(nameof(CreateCustomerUser));
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

            ViewData["ParentOrg"] = new SelectList(parentOrgList.ToList(), "DataValueField", "DataTextField", userCustomerCreateViewModel.ParentOrg);

            List<DataField> userTypeList = new()
            {
                new DataField { DataTextField = "Customer", DataValueField ="Customer"}
            };
            ViewData["UserType"] = new SelectList(userTypeList, "DataValueField", "DataTextField", userCustomerCreateViewModel.UserType);

            return View(userCustomerCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCustomerUserAjax(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _usersCustomerRepository.GetAsync(m => m.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            var model = new UserCustomerUpdateModel
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

            ViewData["ParentOrg"] = new SelectList(parentOrgList, "DataValueField", "DataTextField", result.ParentOrg);

            List<DataField> userTypeList = new()
            {
                new DataField { DataTextField = "Customer", DataValueField ="Customer"}
            };
            ViewData["UserType"] = new SelectList(userTypeList, "DataValueField", "DataTextField", result.UserType);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsCustomerUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _usersCustomerRepository.GetAsync(m => m.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            var model = new UserCustomerUpdateModel
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

            ViewData["ParentOrg"] = new SelectList(parentOrgList, "DataValueField", "DataTextField", result.ParentOrg);

            List<DataField> userTypeList = new()
            {
                new DataField { DataTextField = "Customer", DataValueField ="Customer"}
            };
            ViewData["UserType"] = new SelectList(userTypeList, "DataValueField", "DataTextField", result.UserType);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCustomerUser(UserCustomerUpdateModel userCustomerUpdateModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //TblUserCustomer tblUserCustomer = new TblUserCustomer();
                    string strFilePath = @"Imgs\UsersCustomer";
                    string strFolderPath = @"\Imgs\UsersCustomer\";
                    string webRootPath = _WebHostEnvironment.WebRootPath;
                    var files = HttpContext.Request.Form.Files;
                    var fromDb = _usersCustomerRepository.GetAsync(m => m.Id == userCustomerUpdateModel.Id);

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
                        userCustomerUpdateModel.UploadProfilePic = strFolderPath + fileName + extention_new;
                    }
                    else
                    {
                        userCustomerUpdateModel.UploadProfilePic = fromDb.Result.UploadProfilePic;
                    }

                    if (userCustomerUpdateModel.ActiveStatus)
                    {
                        fromDb.Result.ActiveStatus = "Active";
                    }
                    else
                    {
                        fromDb.Result.ActiveStatus = "DeActive";
                    }

                    if (userCustomerUpdateModel.ActiveStatus)
                    {
                        fromDb.Result.ActiveStatus = "Active";
                    }
                    else
                    {
                        fromDb.Result.ActiveStatus = "DeActive";
                    }

                    fromDb.Result.Id = userCustomerUpdateModel.Id;
                    fromDb.Result.Username = userCustomerUpdateModel.Username;
                    fromDb.Result.FirstName = userCustomerUpdateModel.FirstName;
                    fromDb.Result.MiddleName = userCustomerUpdateModel.MiddleName;
                    fromDb.Result.LastName = userCustomerUpdateModel.LastName;
                    fromDb.Result.ContactNo = userCustomerUpdateModel.ContactNo;
                    fromDb.Result.EmailId = userCustomerUpdateModel.EmailId;
                    fromDb.Result.AdminName = userCustomerUpdateModel.AdminName;
                    fromDb.Result.ParentOrg = userCustomerUpdateModel.ParentOrg;
                    fromDb.Result.UploadProfilePic = userCustomerUpdateModel.UploadProfilePic;
                    fromDb.Result.ValidFromDate = userCustomerUpdateModel.ValidFromDate;
                    fromDb.Result.ValidToDate = userCustomerUpdateModel.ValidToDate;
                    fromDb.Result.UserType = userCustomerUpdateModel.UserType;
                    fromDb.Result.Password = userCustomerUpdateModel.Password;
                    fromDb.Result.UserId = userCustomerUpdateModel.ContactNo;
                    fromDb.Result.SupervisorName = "AvinashK";

                    _usersCustomerRepository.UpdateAsync(fromDb.Result);
                    Notify("Success", "Data updated successfully", "toaster", notificationType: Models.NotificationType.success);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Notify("Error", "Something Missing Or Data Not Found", "toaster", notificationType: Models.NotificationType.error);
                    return RedirectToAction(nameof(CreateCustomerUser));
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

            ViewBag["ParentOrg"] = new SelectList(parentOrgList, "DataValueField", "DataTextField", userCustomerUpdateModel.ParentOrg);

            List<DataField> userTypeList = new()
            {
                new DataField { DataTextField = "Customer", DataValueField ="Customer"}
            };
            ViewData["UserType"] = new SelectList(userTypeList, "DataValueField", "DataTextField", userCustomerUpdateModel.UserType);

            return View(userCustomerUpdateModel);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(short? id)
        {
            var result = await _usersCustomerRepository.GetAsync(m => m.Id == id);

            try
            {
                await _usersCustomerRepository.DeleteAsync(result);
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