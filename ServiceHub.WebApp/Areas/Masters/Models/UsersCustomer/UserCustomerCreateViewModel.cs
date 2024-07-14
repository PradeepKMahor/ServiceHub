using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class UserCustomerCreateViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; } = string.Empty;

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

        [Required]
        [Display(Name = "Parent Org.")]
        public string ParentOrg { get; set; } = string.Empty;

        [Required]
        [Display(Name = "User Type")]
        public string UserType { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Supervisor Name")]
        public string SupervisorName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Admin Name")]
        public string AdminName { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public bool ActiveStatus { get; set; } = false;

        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;
    }
}