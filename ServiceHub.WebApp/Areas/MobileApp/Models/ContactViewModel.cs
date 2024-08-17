using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class ContactViewModel
    {
        [Display(Name = "Address")]
        public string Address { get; set; } = string.Empty;

        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Customer Care")]
        public string CustomerCare { get; set; } = string.Empty;
    }
}