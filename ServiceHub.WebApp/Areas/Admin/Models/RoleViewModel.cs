using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class RoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}