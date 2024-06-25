using ServiceHub.WebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Areas.ServiceRequests.Models
{
    public class ServiceRequestViewModel
    {
        public ServiceRequestViewModel()
        {
            this.FilterDataModel = new FilterDataModel();
        }

        public FilterDataModel FilterDataModel { get; set; }

        [Display(Name = "SRN")]
        public string SRN { get; set; }

        [Display(Name = "Product")]
        public string Product { get; set; }

        [Display(Name = "Customer")]
        public string Customer { get; set; }

        [Display(Name = "Priority")]
        public string Priority { get; set; }

        [Display(Name = "Requested Date")]
        public DateTime? RequestedDate { get; set; }

        [Display(Name = "Requested By Contact")]
        public string RequestedByContact { get; set; }

        [Display(Name = "Assigned To")]
        public string AssignedTo { get; set; }

        [Display(Name = "Serial No")]
        public string SerialNo { get; set; }

        [Display(Name = "Closure Date")]
        public DateTime? ClosureDate { get; set; }

        [Display(Name = "Resolution Time Taken")]
        public string ResolutionTimeTaken { get; set; }

        [Display(Name = "SLA Status")]
        public string SLAStatus { get; set; }
    }
}