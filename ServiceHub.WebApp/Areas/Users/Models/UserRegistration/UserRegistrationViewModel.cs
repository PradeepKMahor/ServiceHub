using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiceHub.WebApp.Areas.Users.Models.UserRegistration
{
    public class UserRegistrationViewModel
    {
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

        [Display(Name = "Valid From Date")]
        public DateTime? ValidFromDate { get; set; }

        [Display(Name = "Valid To Date")]
        public DateTime? ValidToDate { get; set; }

        [Display(Name = "Upload Profile")]
        public string UploadProfilePic { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public string ActiveStatus { get; set; } = string.Empty;
    }
}