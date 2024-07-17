using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class CustomerProductProfileViewModel
    {
        public CustomerProductProfileViewModel()
        {
            this.FilterDataModel = new FilterDataModel();
        }

        public FilterDataModel FilterDataModel { get; set; }

        [Display(Name = "User Id")]
        public string UserId { get; set; }

        [Display(Name = "Customer ID")]
        public string CustomerID { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Supervisor Name")]
        public string SupervisorName { get; set; }

        [Display(Name = "Contact")]
        public string Contact { get; set; }

        [Display(Name = "Valid End Date")]
        public string ValidEndDate { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public string SearchByCustomerID { get; set; } = string.Empty;
        public string SearchByCustomerName { get; set; } = string.Empty;
        public string SearchByStatus { get; set; } = string.Empty;
    }
}