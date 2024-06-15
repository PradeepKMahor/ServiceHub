using ServiceHub.WebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Areas.Masters.Models
{
    public class CustomerUpdateModel
    {
        [Display(Name = "User Id")]
        public string UserId { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Clint Name")]
        public string ClintName { get; set; }

        [Display(Name = "Search")]
        public string RoleId { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [Display(Name = "IsActive")]
        public string IsActive { get; set; }
    }
}