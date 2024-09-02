using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class UserCustomerUpdateModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; } = string.Empty;

        [Display(Name = "User Name")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "You must provide a Contact number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [Display(Name = "Contact No")]
        public string ContactNo { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Email Id")]
        [EmailAddress]
        public string EmailId { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Valid From Date")]
        public DateTime? ValidFromDate { get; set; }

        [Required]
        [Display(Name = "Valid To Date")]
        public DateTime? ValidToDate { get; set; }

        [Display(Name = "Upload Profile")]
        public string UploadProfilePic { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Parent Org.")]
        public string ParentOrg { get; set; } = string.Empty;

        [Required]
        [Display(Name = "User Type")]
        public string UserType { get; set; } = string.Empty;

        [Display(Name = "Supervisor Name")]
        public string SupervisorName { get; set; } = string.Empty;

        [Display(Name = "Admin Name")]
        public string AdminName { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public bool ActiveStatus { get; set; } = false;

        [Display(Name = "One time Password")]
        public string Password { get; set; } = string.Empty;
    }
}