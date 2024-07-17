using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class CustomerUpdateModel
    {
        [Display(Name = "User Id")]
        public string UserId { get; set; } = string.Empty;

        [Display(Name = "User Name")]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Clint Name")]
        public string ClintName { get; set; } = string.Empty;

        [Display(Name = "Search")]
        public string RoleId { get; set; } = string.Empty;

        [Display(Name = "Role Name")]
        public string RoleName { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public string IsActive { get; set; } = string.Empty;
    }
}