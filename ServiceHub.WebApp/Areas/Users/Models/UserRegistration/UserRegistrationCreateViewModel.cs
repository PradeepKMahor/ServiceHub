using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class UserRegistrationCreateViewModel
    {
        //[Required]
        //[Display(Name = "UserID")]
        //public string UserId { get; set; } = string.Empty;

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "User Name")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Contact No")]
        public string ContactNo { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Email Id")]
        public string EmailId { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Valid From Date")]
        public DateTime? ValidFromDate { get; set; }

        [Required]
        [Display(Name = "Valid To Date")]
        public DateTime? ValidToDate { get; set; }

        [Required]
        [Display(Name = "Upload Profile")]
        public string UploadProfilePic { get; set; } = string.Empty;
    }
}