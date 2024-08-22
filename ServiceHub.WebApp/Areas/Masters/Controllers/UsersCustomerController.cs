using Microsoft.AspNetCore.Mvc;
using ServiceHub.DataAccess;
using ServiceHub.Domain.Models.Data;
using ServiceHub.WebApp.Controllers;
using ServiceHub.WebApp.Models;

namespace ServiceHub.WebApp.Areas.Masters.Controllers
{
    [Area("Masters")]
    public class UsersCustomerController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersCustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var userCustomerViewModel = new UserCustomerViewModel();
            return View(userCustomerViewModel);
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

                    //if (User.Identity.Name == null)
                    //{
                    //    userCustomerCreateViewModel.StaffInfo.CrBy = "Admin";
                    //}
                    //else
                    //{
                    //    userCustomerCreateViewModel.StaffInfo.CrBy = User.Identity.Name;
                    //}
                    //userCustomerCreateViewModel.StaffInfo.CrOn = DateTime.Now;

                    if (userCustomerCreateViewModel.Id == 0)
                    {
                        _unitOfWork.UserCustomer.Add(tblUserCustomer);
                        _unitOfWork.Save();
                        Notify("Success", "Data saved successfully", "toaster", notificationType: Models.NotificationType.success);

                        return RedirectToAction(nameof(CreateCustomerUser));
                    }
                    else
                    {
                        _unitOfWork.UserCustomer.Update(tblUserCustomer);
                        _unitOfWork.Save();
                        Notify("Success", "Data updated successfully", "toaster", notificationType: Models.NotificationType.success);
                        return RedirectToAction(nameof(Index));
                    }
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
    }
}