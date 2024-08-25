using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServiceHub.DataAccess;
using ServiceHub.DataAccess.Base;
using ServiceHub.DataAccess.Repositories.Core;
using ServiceHub.Domain.Context;
using ServiceHub.Domain.Models.Data;
using ServiceHub.WebApp.Controllers;
using ServiceHub.WebApp.Models;
using static ServiceHub.WebApp.Models.DTModel;

namespace ServiceHub.WebApp.Areas.Masters.Controllers
{
    [Area("Masters")]
    public class UsersCustomerController : BaseController
    {
        private readonly DataContext _dataContext;
        private readonly IUsersCustomerRepository _usersCustomerRepository;

        public UsersCustomerController(DataContext dataContext, IUsersCustomerRepository usersCustomerRepository)
        {
            _dataContext = dataContext;
            _usersCustomerRepository = usersCustomerRepository;
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
                    TblUserCustomer tblUserCustomer = new TblUserCustomer();
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
                    tblUserCustomer.LastName = userCustomerCreateViewModel.LastName;
                    tblUserCustomer.ContactNo = userCustomerCreateViewModel.ContactNo;
                    tblUserCustomer.EmailId = userCustomerCreateViewModel.EmailId;
                    tblUserCustomer.AdminName = userCustomerCreateViewModel.AdminName;
                    tblUserCustomer.ParentOrg = userCustomerCreateViewModel.ParentOrg;
                    tblUserCustomer.UploadProfilePic = userCustomerCreateViewModel.UploadProfilePic;
                    tblUserCustomer.ValidFromDate = userCustomerCreateViewModel.ValidFromDate;
                    tblUserCustomer.ValidToDate = userCustomerCreateViewModel.ValidToDate;
                    tblUserCustomer.UserType = userCustomerCreateViewModel.UserType;
                    tblUserCustomer.SupervisorName = "AvinashK";

                    //if (User.Identity.Name == null)
                    //{
                    //    userCustomerCreateViewModel.StaffInfo.CrBy = "Admin";
                    //}
                    //else
                    //{
                    //    userCustomerCreateViewModel.StaffInfo.CrBy = User.Identity.Name;
                    //}
                    //userCustomerCreateViewModel.StaffInfo.CrOn = DateTime.Now;

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

            return View(userCustomerCreateViewModel);
            //try
            //{
            //    if (ModelState.IsValid)
            //    {
            //        Notify("Success", "Data saved successfully", "toaster", notificationType: NotificationType.success);

            //        return RedirectToAction(nameof(Index));
            //    }
            //    else
            //    {
            //        Notify("Error", "Something Missing Or Data Not Found", "toaster", notificationType: NotificationType.error);
            //        return RedirectToAction(nameof(Index));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Notify("Error", ex.Message, "toaster", notificationType: NotificationType.error);
            //}

            //return View(userCustomerCreateViewModel);
        }

        public async Task<IActionResult> UpdateCustomerUserAsync(int id)
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
                LastName = result.LastName,
                Id = result.Id,
                ParentOrg = result.ParentOrg,
                SupervisorName = result.SupervisorName,
                UploadProfilePic = result.UploadProfilePic,
                Username = result.Username,
                UserType = result.UserType,
                ValidFromDate = result.ValidFromDate,
                ValidToDate = result.ValidToDate,
                MiddleName = ""
            };
            if (result.ActiveStatus == "Active")
            {
                model.ActiveStatus = true;
            }
            else
            {
                model.ActiveStatus = false;
            }
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
                    TblUserCustomer tblUserCustomer = new TblUserCustomer();
                    if (userCustomerUpdateModel.ActiveStatus)
                    {
                        tblUserCustomer.ActiveStatus = "Active";
                    }
                    else
                    {
                        tblUserCustomer.ActiveStatus = "DeActive";
                    }

                    tblUserCustomer.Username = userCustomerUpdateModel.Username;
                    tblUserCustomer.FirstName = userCustomerUpdateModel.FirstName;
                    tblUserCustomer.LastName = userCustomerUpdateModel.LastName;
                    tblUserCustomer.ContactNo = userCustomerUpdateModel.ContactNo;
                    tblUserCustomer.EmailId = userCustomerUpdateModel.EmailId;
                    tblUserCustomer.AdminName = userCustomerUpdateModel.AdminName;
                    tblUserCustomer.ParentOrg = userCustomerUpdateModel.ParentOrg;
                    tblUserCustomer.UploadProfilePic = userCustomerUpdateModel.UploadProfilePic;
                    tblUserCustomer.ValidFromDate = userCustomerUpdateModel.ValidFromDate;
                    tblUserCustomer.ValidToDate = userCustomerUpdateModel.ValidToDate;
                    tblUserCustomer.UserType = userCustomerUpdateModel.UserType;
                    tblUserCustomer.SupervisorName = "AvinashK";

                    _usersCustomerRepository.UpdateAsync(tblUserCustomer);
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

            return View(userCustomerUpdateModel);
        }
    }
}