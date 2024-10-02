using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class CustomerProductProfileDetailViewModel
    {
        public CustomerProductProfileDetailViewModel()
        {
            this.FilterDataModel = new FilterDataModel();
        }

        public ProductViewModel? productViewModel { get; set; }
        public UserCustomerViewModel? userCustomerViewModel { get; set; }
        public UserRegistrationViewModel? userRegistrationViewModel { get; set; }

        public FilterDataModel FilterDataModel { get; set; }

        [Display(Name = "CustomerID")]
        public string CustomerID { get; set; } = string.Empty;

        [Display(Name = "Customer/Organization name")]
        public string CustomerName { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Supervisor Name")]
        public string SupervisorName { get; set; }

        [Display(Name = "Contact No. 1")]
        public string Contact1 { get; set; }

        [Display(Name = "Contact No. 2")]
        public string Contact2 { get; set; }

        [Display(Name = "Valid End Date")]
        public string ValidEndDate { get; set; }

        [Display(Name = "Email 1")]
        public string Email1 { get; set; }

        [Display(Name = "Email 2")]
        public string Email2 { get; set; }

        [Display(Name = "Customer GST")]
        public string CustomerGST { get; set; }

        [Display(Name = "Customer Address")]
        public string CustomerAddress { get; set; }

        [Display(Name = "Valid From Date")]
        public string ValidFromDate { get; set; }

        [Display(Name = "Valid To Date")]
        public string ValidToDate { get; set; }

        [Display(Name = "Country Code")]
        public string CountryCode { get; set; }

        [Display(Name = "Contact No.")]
        public string ContactNo { get; set; }

        public string CountProducts { get; set; }
        public string CountUsers { get; set; }
        public string CountPMRs { get; set; }
        public string CMRs { get; set; }
        public string WMRs { get; set; }

        public string SearchByCustomerID { get; set; } = string.Empty;
        public string SearchByCustomerName { get; set; } = string.Empty;
        public string SearchByStatus { get; set; } = string.Empty;
    }
}