using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class UserCustomerViewModel
    {
        public UserCustomerViewModel()
        {
            this.FilterDataModel = new FilterDataModel();
        }

        public FilterDataModel FilterDataModel { get; set; }

        [Display(Name = "UserID")]
        public string UserId { get; set; } = string.Empty;

        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "User Name")]
        public string Username { get; set; } = string.Empty;

        [Display(Name = "Contact No")]
        public string ContactNo { get; set; } = string.Empty;

        [Display(Name = "Email Id")]
        public string EmailId { get; set; } = string.Empty;

        [Display(Name = "Parent Org.")]
        public string ParentOrg { get; set; } = string.Empty;

        [Display(Name = "User Type")]
        public string UserType { get; set; } = string.Empty;

        [Display(Name = "Supervisor Name")]
        public string SupervisorName { get; set; } = string.Empty;

        [Display(Name = "Admin Name")]
        public string AdminName { get; set; } = string.Empty;

        [Display(Name = "Valid From Date")]
        public DateTime? ValidFromDate { get; set; }

        [Display(Name = "Valid To Date")]
        public DateTime? ValidToDate { get; set; }

        [Display(Name = "Upload Profile")]
        public string UploadProfilePic { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public string ActiveStatus { get; set; } = string.Empty;

        public string SearchByFirstLastName { get; set; } = string.Empty;
        public string SearchByUsername { get; set; } = string.Empty;
        public string SearchByParentOrg { get; set; } = string.Empty;
        public string SearchByStatus { get; set; } = string.Empty;
        public string SearchByUserType { get; set; } = string.Empty;
    }
}