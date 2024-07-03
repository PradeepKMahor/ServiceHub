using ServiceHub.WebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Areas.Masters.Models
{
    public class CustomerViewModel
    {
        public CustomerViewModel()
        {
            this.FilterDataModel = new FilterDataModel();
        }

        public FilterDataModel FilterDataModel { get; set; }

        [Display(Name = "CID")]
        public string CID { get; set; } = string.Empty;

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public string Status { get; set; } = string.Empty;

        [Display(Name = "Supervisor name")]
        public string SupervisorName { get; set; } = string.Empty;

        [Display(Name = "Contact")]
        public string Contact { get; set; } = string.Empty;

        [Display(Name = "Valid end date")]
        public string ValidEndDate { get; set; } = string.Empty;

        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "IsActive")]
        public string IsActive { get; set; } = string.Empty;
    }
}