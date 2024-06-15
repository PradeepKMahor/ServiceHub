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
        public string CID { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Supervisor name")]
        public string SupervisorName { get; set; }

        [Display(Name = "Contact")]
        public string Contact { get; set; }

        [Display(Name = "Valid end date")]
        public string ValidEndDate { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "IsActive")]
        public string IsActive { get; set; }
    }
}