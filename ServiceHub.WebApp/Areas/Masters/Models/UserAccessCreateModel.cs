using ServiceHub.WebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Areas.Masters.Models
{
    public class UserAccessCreateModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Clint Name")]
        public string ClintName { get; set; }

        [Display(Name = "Search")]
        public string RoleId { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [Display(Name = "IsActive")]
        public string IsActive { get; set; }
    }
}