using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class UserCreateViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Contact No.")]
        public string ContactNo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Enter Password")]
        public string EnterPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Status")]
        public string IsActive { get; set; }

        public string UploadProfilePic { get; set; }
    }
}